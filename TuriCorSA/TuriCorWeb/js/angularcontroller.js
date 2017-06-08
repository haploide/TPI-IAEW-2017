var module = angular.module('app', ['ngRoute'])
    .config(function($routeProvider){
        $routeProvider.when("/", {
            controller: "homeController as home",
            templateUrl: "/templates/homeView.html"
        }).when("/listarvehiculos", {
                controller: "vehiculosController as vehiculosList",
                templateUrl: "templates/VehiculosView.html"

        }).otherwise({
            redirectTo: "/"
        });
    }).controller('homeController', function ($http) {
        var homeController = this;
        homeController.title = "TuriCor S.A.";

    }).controller('vehiculosController', function ($http, $scope) {
        var vehiculosController = this;
        vehiculosController.title = 'Consulta de Vehiculos Disponibles';
        vehiculosController.vehiculos = [];
        vehiculosController.paises = [];
        vehiculosController.ciudades = [];
        vehiculosController.buscarVehiculos = function () {
            vehiculosController.isBusy = true;
            $http({
                method: 'GET',
                url: 'http://localhost:2253/api/vehiculo?Id=2&fechaHoraRetiro=' + $scope.fechaDesde + '&fechaHoraDevolucion=' + $scope.fechaHasta,
                headers: {
                    'Accept': "application/json"
                }
            }).then(function (response) {
                angular.copy(response.data.VehiculosEncontrados, vehiculosController.vehiculos);
                vehiculosController.isBusy = false;

            }, function (response) {
                alert("Error");
            }).then(function () {

            });
        }
        vehiculosController.buscarCiudades = function () {
            vehiculosController.isBusy = true;
            $http({
                method: 'GET',
                url: 'http://localhost:2253/api/ciudad?id=1',
                headers: {
                    'Accept': "application/json"
                }
            }).then(function (response) {
                angular.copy(response.data.Ciudades, vehiculosController.ciudades);
                vehiculosController.isBusy = false;

            }, function (response) {
                alert("Error");
            }).then(function () {

            });
        }
        vehiculosController.isBusy = true;
        $http({
            method: 'GET',
            url: 'http://localhost:2253/api/pais',
            headers: {
                'Accept': "application/json"
            }
        }).then(function (response) {
            angular.copy(response.data.Paises, vehiculosController.paises);
            vehiculosController.isBusy = false;

        }, function (response) {
            alert("Error");
        }).then(function () {

        });

    })
