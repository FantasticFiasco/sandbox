function add(str) {
    var numbers = str.split(',');
    var sum = 0;

    for (var i = numbers.length; i--;) {
        sum += parseInt(numbers[i] || 0, 10);
    }

    return sum;
}