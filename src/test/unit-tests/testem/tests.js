describe('add', function() {

    it('returns 0 for ""', function() {
        expect(add('')).toBe(0);
    });

    it('return 1 for "1"', function() {
        expect(add('1')).toBe(1);
    });

    it('return 3 for "1,2"', function() {
        expect(add('1,2')).toBe(3);
    });
});