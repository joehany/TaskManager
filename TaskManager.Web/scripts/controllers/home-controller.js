taskManager.controller("homeController", function ($scope, listRepository, $location) {
    listRepository.createNewList().then(function (list) {
        $location.path('/list/'+list.Id);
    });
});