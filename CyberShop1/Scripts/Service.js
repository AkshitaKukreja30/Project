/// <reference path="../app.js" />


CyberShop.service('myService', function ($http) {
    //Create new record
    this.post = function (a) {
        var request = $http({
            method: "post",
            url: "/api/RegistrationDetails",
            data: a

        });

        return request;
    }



    this.checkUserExists = function (foruser) {
        return $http.get("/api/RegistrationDetails/GetRegistrationDetail?username=" + foruser.User_Name);

    };


    this.checkLogin = function (foruserlogin) {
        
        return $http.get("/api/RegistrationDetails/GetRegistrationDetail2?username=" + foruserlogin.UserNameForLogin
            + "&password=" + foruserlogin.PasswordForLogin);
        debugger
    };








    //promisePost.then(function (pl) {
        //    $scope.EmpNo = pl.data.EmpNo;
        //    loadRecords();
        //}, function (err) {
        //    console.log("Err" + err);
        //});

});