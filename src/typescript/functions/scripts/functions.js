var demo;
(function (demo) {
    var squareItSimple = function (h, w) {
        return h * w;
    };

    var squareItSimplest = function (h, w) {
        return h * w;
    };

    var helloWorld;
    helloWorld = function (name) {
        console.log('Hello ' + (name || 'unknown person'));
    };

    helloWorld();
    helloWorld('John');

    var squareIt;
    var rectA = { h: 7 };
    var rectB = { h: 7, w: 12 };

    squareIt = function (rect) {
        if (rect.w === undefined) {
            return rect.h * rect.h;
        }

        return rect.h * rect.w;
    };

    console.log(squareIt(rectA));
    console.log(squareIt(rectB));
})(demo || (demo = {}));
