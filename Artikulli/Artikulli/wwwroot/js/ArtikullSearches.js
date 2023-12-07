const getSearch = (name) => {
    var settings = {
        "url": `/UserInterface/GetSearch?name=${name}`,
        "method": "GET",
        "headers": {
            "Content-Type": "application/json"
        },
    };
    $.ajax(settings).done(function (response) {
        searchContainer.html(response)
    });
}


var searchField = $("#searchBox")


var searchButton = $("#searchButton")
var searchContainer = $("#searchContainer")


var timeout;
searchField.on('keyup', function (e) {
    clearTimeout(timeout);

    searchContainer.html("<h2>getting searches...</h2>")
    timeout = setTimeout(() => {
        searchNames()
    }, 500)

});

function searchNames() {
    var searches = searchField.val()
    searchContainer.empty()
    searchContainer.html("<h2>getting searches...</h2>")
    getSearch(searches)
}

searchButton.on("click", function () {
    searchNames()
});