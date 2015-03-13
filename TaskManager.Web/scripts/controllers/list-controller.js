taskManager.controller("listController", function ($scope, listRepository, $routeParams, $location) {
    $scope.gettingList = true;
    var id = $routeParams.id;
    if (id == undefined) {
        $location.path('/');

    } else {
        listRepository.getListById(id).then(function (list) {
            $scope.gettingList = false;
            if (list != "null") {
                $scope.list = list;
                localStorage.setItem("list", list.Id);
                list.DateModified += " GMT";
                list.DateCreated += " GMT";

            } else {
                $scope.error = true;
                $scope.errorTitle = "Ooops!";
                $scope.errorMessage = "We didn't find a list with such id: " + id + "....  Please hit \"New List\" button to create a new list";
            }

        },
            function (error) {
                $scope.error = true;
                $scope.errorTitle = "Server Error!";
                $scope.errorMessage = error.Message;
            });
    }
    $scope.saveItem = function () {
        $scope.newItem.TaskListId = $scope.list.Id;
        listRepository.createNewTask($scope.newItem).then(
            function (createdItem) {
                $scope.list.Items.push(createdItem);
                $scope.list.DateModified = new Date().toJSON();
            },
            function (error) {
                $scope.error = true;
                $scope.errorTitle = "Server Error!";
                $scope.errorMessage = error;
            });

    };
    $scope.createNewList = function () {
        listRepository.createNewList().then(function (list) {
            $location.path('/list/' + list.Id);
        });
    };
    $scope.updateTaskName = function (name, task) {
        if (name == "")
            return "Please enter task name";
        if (name != task.Name) {
            task.Name = name;
            listRepository.updateTask(task).then(function () {
                $scope.list.DateModified = new Date().toJSON();
            },
            function (error) {
                $scope.error = true;
                $scope.errorTitle = "Server Error!";
                $scope.errorMessage = error;
            });
        }

    };
    $scope.deleteTask = function (task) {
        listRepository.deleteTask(task.Id).then(function () {
            var index = $scope.list.Items.indexOf(task);
            if (index > -1) {
                $scope.list.DateModified = new Date().toJSON();
                $scope.list.Items.splice(index, 1);
            }
        },
            function (error) {
                $scope.error = true;
                $scope.errorTitle = "Server Error!";
                $scope.errorMessage = error;
            });
    };
    $scope.changeTaskStatus = function (task) {
        listRepository.updateTask(task).then(function () {
            $scope.list.DateModified = new Date().toJSON();
        },
            function (error) {
                $scope.error = true;
                $scope.errorTitle = "Server Error!";
                $scope.errorMessage = error.value;
            });
    };
    $scope.doneTasks = function () {
        try {
            var checked = 0;
            for (var i = 0; i < $scope.list.Items.length; i++) {
                if ($scope.list.Items[i].IsDone)
                    checked++;
            }
            return checked;
        }
        catch (ex) {
            return 0;
        }
    };
    $scope.getPercentage = function () {
        try {
            if ($scope.list.Items != null)
                return Math.floor(($scope.doneTasks() / $scope.list.Items.length) * 100);
            return 0;
        } catch (ex) {
            return 0;
        }
    };


});