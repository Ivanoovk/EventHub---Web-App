


document.addEventListener("DOMContentLoaded", function () {
    let searchBar = document.getElementById("searchBar");
    let cityFilter = document.getElementById("cityFilter");

    function filterPlaces() {
        let searchValue = searchBar.value.toLowerCase();
        let cityValue = cityFilter.value.toLowerCase();
        let placeCards = document.querySelectorAll(".place-card");

        placeCards.forEach(place => {
            let placeName = place.querySelector(".card-title").textContent.toLowerCase();
            let placeCity = place.getAttribute("data-city").toLowerCase();

            let matchesSearch = searchValue === "" || placeName.includes(searchValue);
            let matchesCity = cityValue === "" || placeCity === cityValue;

            if (matchesSearch && matchesCity) {
                place.style.display = "block";
            } else {
                place.style.display = "none";
            }
        });
    }

    searchBar.addEventListener("keyup", filterPlaces);
    cityFilter.addEventListener("change", filterPlaces);
});