/// <reference path="../app.js" />


CyberShop.service('myService', function ($http) {
    //Create new record
    this.post = function (a) {
        var request = $http({
            method: "post",
            url: "/api/RegistrationDetails",
            data:a
        });
        return request;
    }
});