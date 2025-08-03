

$(document).ready(function () {
    $(".buy-ticket-btn").on("click", function () {
        const placeId = $(this).attr("data-place-id");
        const placeName = $(this).attr("data-place-name");
        const eventId = $(this).attr("data-event-id");
        const eventName = $(this).attr("data-event-name");

        console.log("Place ID:", placeId);
        console.log("Place Name:", placeName);
        console.log("Event ID:", eventId);
        console.log("Event Name:", eventName);

        if (!placeId || !eventId) {
            Swal.fire("Error!", "Missing Place ID or Event ID.", "error");
            return;
        }

        $("#placeId").val(placeId);
        $("#eventId").val(eventId);
        $("#placeNamePlaceholder").text(placeName);

        $("#buyTicketModalLabel").html(`Buy Ticket - ${placeName} <br><small class="text-muted">${eventName}</small>`);

        $.ajax({
            url: "https://localhost:7172/PlaceEventApi/Showtimes", /*!!!!!!!!!!!!!!!*/
            method: "GET",
            data: {
                placeId: placeId,
                eventId: eventId,
            },
            xhrFields: {
                withCredentials: true 
            },
            success: function (showtimes) {
                console.log("Success Response:", showtimes); // ✅ Log the success response

                const showtimesSelector = document.getElementById('showtimeSelect');
                showtimesSelector.innerHTML = '<option value="" disabled selected>Choose a time</option>';
                showtimes.forEach(time => {
                    const optionSubtag = document.createElement('option');

                    optionSubtag.value = time;
                    optionSubtag.textContent = time;

                    showtimesSelector.appendChild(optionSubtag);
                });

                $("#buyTicketModal").modal("show");
            },
            error: function (xhr) {
                let errorMessage = "An error occurred while fetching showtimes.";
                console.error("Raw Response:", xhr.responseText); // Log the raw response

                try {
                    if (xhr.responseJSON) {
                        errorMessage = xhr.responseJSON.title || xhr.responseJSON.message || errorMessage;
                    } else if (xhr.responseText) {
                        errorMessage = xhr.responseText; // Use response as-is if it's plain text
                    }
                } catch (e) {
                    console.error("Error processing response:", e);
                }

                Swal.fire("Error!", errorMessage, "error");
            }
        })
    });

    $('#showtimeSelect').change(function () {
        const selectedVal = $(this).val();
        const placeId = $("#placeId").val();
        const eventId = $("#eventId").val();

        if (selectedVal !== "") {
            $("#availableTicketsDiv").prop("hidden", false);

            $.ajax({
                url: "https://localhost:7172/PlaceEventApi/AvailableTickets", /*!!!!!!!!!!!!!!*/
                method: "GET",
                data: {
                    placeId: placeId,
                    eventId: eventId,
                    showtime: selectedVal,
                },
                xhrFields: {
                    withCredentials: true
                },
                success: function (availableTicketsCount) {
                    console.log("Success Response:", availableTicketsCount); // ✅ Log the success response

                    $('#availableTicketsCount').text(availableTicketsCount);
                    $('#buyTicketButton').prop("disabled", false);
                },
                error: function (xhr) {
                    let errorMessage = "An error occurred while fetching showtimes.";
                    console.error("Raw Response:", xhr.responseText); // Log the raw response

                    try {
                        if (xhr.responseJSON) {
                            errorMessage = xhr.responseJSON.title || xhr.responseJSON.message || errorMessage;
                        } else if (xhr.responseText) {
                            errorMessage = xhr.responseText; // Use response as-is if it's plain text
                        }
                    } catch (e) {
                        console.error("Error processing response:", e);
                    }

                    Swal.fire("Error!", errorMessage, "error");
                }
            });
        }
    });

    $("#buyTicketButton").on("click", function () {
        const requestData = {
            PlaceId: $("#placeId").val().trim(),
            EventId: $("#eventId").val().trim(),
            Showtime: $("#showtimeSelect").val().trim(),
            Quantity: parseInt($("#quantity").val(), 10)
        };

        console.log("Submitting Request:", requestData); // ✅ Added for Debugging

        if (!requestData.Quantity || requestData.Quantity < 1) {
            $("#errorMessage").text("Please enter a valid ticket quantity.").removeClass("d-none");
            return;
        }

        $.ajax({
            url: "https://localhost:7180/api/internal/TicketApi/Buy", /*!!!!!!!!!!!!!!!!!!*/
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(requestData),
            xhrFields: {
                withCredentials: true,
            },
            success: function (response) {
                console.log("Success Response:", response); // ✅ Log the success response
                Swal.fire("Success!", "Your ticket has been purchased successfully!", "success");
                $("#buyTicketModal").modal("hide");
                $('#availableTicketsCount').prop("hidden", true);
            },
            error: function (xhr) {
                let errorMessage = "An error occurred while purchasing tickets.";
                console.error("Raw Response:", xhr.responseText); // Log the raw response

                try {
                    if (xhr.responseJSON) {
                        errorMessage = xhr.responseJSON.title || xhr.responseJSON.message || errorMessage;
                    } else if (xhr.responseText) {
                        errorMessage = xhr.responseText; // Use response as-is if it's plain text
                    }
                } catch (e) {
                    console.error("Error processing response:", e);
                }

                Swal.fire("Error!", errorMessage, "error");
            }

        });
    });
});