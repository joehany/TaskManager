taskManager.controller("homeController", function ($scope, listRepository, $location) {
    var storedList = localStorage.getItem("list");
    if (storedList == null) {
        listRepository.createNewList().then(function (list) {
            $location.path('/list/' + list.Id);
        });
    } else {
        $location.path('/list/' + storedList);

    }
});