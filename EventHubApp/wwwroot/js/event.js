document.addEventListener("DOMContentLoaded", function () {
    console.log("✅ DOM fully loaded and parsed.");

    const modalElement = document.getElementById("eventDetailsModal");
    if (!modalElement) {
        console.error("❌ Error: Modal element #eventDetailsModal not found!");
        return;
    }

    const eventDetailsModal = new bootstrap.Modal(modalElement);
    const detailsContainer = document.getElementById("eventDetailsContent");

    // Attach event listeners to all 'View Details' buttons
    const viewDetailsButtons = document.querySelectorAll(".view-details-btn");
    if (viewDetailsButtons.length === 0) {
        console.warn("⚠️ No '.view-details-btn' elements found on the page.");
    }

    viewDetailsButtons.forEach(button => {
        button.addEventListener("click", function (event) {
            event.preventDefault();

            let eventId = this.getAttribute("data-event-id");
            console.log(`Fetching details for event ID: ${eventId}`);

            fetch(`/Event/DetailsPartial/${eventId}`)
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`❌ Failed to load event details! Status: ${response.status}`);
                    }
                    return response.text();
                })
                .then(data => {
                    console.log("📥 Event details received:", data);

                    detailsContainer.innerHTML = data;

                    let eventTitleElement = detailsContainer.querySelector("h3");
                    let eventTitle = eventTitleElement ? eventTitleElement.textContent : "Event Details";
                    document.getElementById("eventDetailsLabel").textContent = eventTitle;

                    let modalWatchlistBtn = document.getElementById("add-to-watchlist-btn");

                    if (modalWatchlistBtn) {
                        modalWatchlistBtn.setAttribute("data-event-id", eventId);

                        // Check if the current page is Watchlist, then hide the button
                        if (window.location.pathname.includes("/Watchlist")) {
                            modalWatchlistBtn.style.display = "none";
                        } else {
                            // Make AJAX request to check if the event is already in the watchlist
                            fetch(`/Watchlist/IsEventInWatchlist/${eventId}`)
                                .then(response => response.json())
                                .then(isInWatchlist => {
                                    if (isInWatchlist) {
                                        modalWatchlistBtn.style.display = "none";
                                    } else {
                                        modalWatchlistBtn.style.display = "inline-block";
                                    }
                                })
                                .catch(error => {
                                    console.error("⚠️ Error checking watchlist status:", error);
                                });
                        }
                    }

                    detailsContainer.style.display = "block";
                    eventDetailsModal.show();

                    attachDynamicEventListeners();
                })
                .catch(error => {
                    console.error("❌ Error loading event details:", error);
                    detailsContainer.innerHTML = `<p class="text-danger">Failed to load event details. Please try again later.</p>`;
                });
        });
    });

    function attachDynamicEventListeners() {
        setTimeout(() => {
            console.log("🔄 Attaching dynamic event listeners...");

            $("#add-to-watchlist-btn").off("click").on("click", function () {
                let eventId = $(this).data("event-id");

                if (!eventId) {
                    Swal.fire("Error!", "Event ID is missing.", "error");
                    return;
                }

                $.post("/Watchlist/Add", { eventId: eventId })
                    .done(function () {
                        Swal.fire({
                            title: "Added!",
                            text: "The event has been added to your watchlist.",
                            icon: "success",
                            confirmButtonColor: "#28a745"
                        });

                        $("#add-to-watchlist-btn").hide(); // Hide button after adding
                    })
                    .fail(function () {
                        Swal.fire({
                            title: "Error!",
                            text: "Failed to add the event to your watchlist. Please try again.",
                            icon: "error",
                            confirmButtonColor: "#dc3545"
                        });
                    });
            });

        }, 200);
    }
});

// jQuery for "Add to Watchlist" buttons in the Movie List
$(document).ready(function () {
    $(".add-to-watchlist-btn").on("click", function () {
        const eventId = $(this).data("event-id");

        if (!eventId) {
            Swal.fire("Error!", "Event ID is missing.", "error");
            return;
        }

        $.post("/Watchlist/Add", { eventId: eventId })
            .done(function () {
                Swal.fire({
                    title: "Added!",
                    text: "The event has been added to your watchlist.",
                    icon: "success",
                    confirmButtonColor: "#28a745"
                });
            })
            .fail(function () {
                Swal.fire({
                    title: "Error!",
                    text: "Failed to add the event to your watchlist. Please try again.",
                    icon: "error",
                    confirmButtonColor: "#dc3545"
                });
            });
    });
});