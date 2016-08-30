var Loggers;
(function (Loggers) {
    var Logger = (function () {
        function Logger() {
        }
        Logger.prototype.log = function (msg) {
            console.log(msg);
        };
        return Logger;
    }());
    Loggers.Logger = Logger;
})(Loggers || (Loggers = {}));
