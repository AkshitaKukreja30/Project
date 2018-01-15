var CyberShop = angular.module('CyberShop', []);

CyberShop.controller('cyberController', function ($scope, myService) {
    $scope.message = "quick";
    //var UserData = {
    //    First_Name: $scope.fn,
    //    Last_Name: $scope.sn,
    //    Phone_Num: $scope.pn,
    //    Pincode: $scope.pc,
    //    Address: $scope.adr,
    //    Email_id: $scope.e,
    //    User_Name: $scope.u,
    //    Password: $scope.p,
    //    ConfirmPassword: $scope.cp
    //};

      $scope.PostRegistrationDetail = function () {
        // here comes the power ;-)
        var a = $scope.a;

       var promisePost = myService.post(a);
    }
});
   
      