const express = require('express');
const app = express();

app.use(express.json());
app.use(validate);

app.post('/login', (req, res) => {
    console.log(`login - ${req.body.username}/${req.body.password}`);

    res.send(`Hello ${req.body.username}!`);
});

app.post('/logout', (req, res) => {
    console.log(`logout - ${req.body.username}/${req.body.password}`);

    res.send(`Goodbye ${req.body.username}!`);
});

app.get('/', (req, res) => {
    console.log('index');
    
    res.send('<html><body><p>Index</p></body></html>');
});

app.get('/profile', (req, res) => {
    console.log('profile');
    
    res.send('<html><body><p>Profile</p></body></html>');
});

app.listen(3000, () => console.log('Example app listening on port 3000!'));

function validate(req, res, next) {
    if (req.method === 'POST' && req.header('content-type') !== 'application/json') {
        res.sendStatus(415);
    } else {
        next();
    }
}