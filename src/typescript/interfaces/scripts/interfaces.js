var demo;
(function (demo) {
    var squareBasic = function (num) {
        return num * num;
    };

    

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

    

    var p = {
        name: 'Coleen',
        age: 40,
        kids: 4,
        calcPets: function () {
            return this.kids * 2;
        },
        makeYounger: function (years) {
            this.age -= years;
        },
        greet: function (msg) {
            return msg + ', ' + this.name;
        }
    };

    var pets = p.calcPets();
    console.log(pets);
    p.makeYounger(15);
    var newAge = p.age;
    console.log(newAge);

    var msg = p.greet('Good day to you');
    console.log(msg);
})(demo || (demo = {}));
