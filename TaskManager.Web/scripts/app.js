var taskManager = angular.module("taskManager", ['ngRoute']);
taskManager.config(function ($routeProvider) {
        $routeProvider
            .when('/list/:id',
                {
                    templateUrl: '/templates/list.html',
                    controller: 'listController'
                })
            .when('/',
                {
                    templateUrl: '/templates/home.html',
                    controller: 'homeController'
                });

    });