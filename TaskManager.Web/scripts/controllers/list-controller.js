taskManager.controller("listController", function($scope, listRepository, $routeParams) {
    var id = $routeParams.id;
    if (id == undefined) {
        //todo:error
    } else {
        listRepository.getListById(id).then(function (list) { $scope.list = list; });
    }
    $scope.saveItem = function () {
        $scope.newItem.listId = $scope.list.Id;
        listRepository.createNewItem($scope.newItem).then(function (createdItem) {
            $scope.list.Items.push(createdItem);
        });
        ;
    };
});