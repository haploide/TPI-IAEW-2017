var module = angular.module('app', ['ngRoute'])
    .config(function($routeProvider){
        $routeProvider.when("/", {
            controller: "homeController as home",
            templateUrl: "/templates/homeView.html"
        }).when("/listarvehiculos", {
                controller: "vehiculosController as vehiculosList",
                templateUrl: "templates/VehiculosView.html"

        }).when("/listarreservas", {
            controller: "reservasController as reservasList",
            templateUrl: "templates/ReservasView.html"

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
        vehiculosController.seleccionarCiudad=function (id) {
            vehiculosController.ciudadSeleccionada = id;
        };
        
        vehiculosController.buscarVehiculos = function () {
            vehiculosController.isBusy = true;
            $http({
                method: 'GET',
                url: 'http://localhost:2253/api/vehiculo?Id=' + vehiculosController.ciudadSeleccionada + '&fechaHoraRetiro=' + $scope.fechaDesde + '&fechaHoraDevolucion=' + $scope.fechaHasta,
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
        vehiculosController.buscarCiudades = function (pais) {
            //alert(pais);
            vehiculosController.isBusy = true;
            $http({
                method: 'GET',
                url: 'http://localhost:2253/api/ciudad?id='+pais.Id,
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
            $('#cmbCiudad').removeAttr('disabled');
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
        

    }).controller('reservasController', function ($http, $scope) {
        var reservasController = this;
        reservasController.title = 'Consulta de reservas realizadas';
        reservasController.reservas = [];

        reservasController.buscarReservas = function () {
            reservasController.isBusy = true;
            $http({
                method: 'GET',
                url: 'http://localhost:2253/api/Reserva',
                headers: {
                    'Accept': "application/json"
                }
            }).then(function (response) {
                angular.copy(response.data, reservasController.reservas);
                reservasController.isBusy = false;

            }, function (response) {
                alert("Error");
            }).then(function () {

            });
        }

    })


