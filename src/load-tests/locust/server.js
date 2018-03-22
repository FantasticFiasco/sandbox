const express = require('express');
const app = express();

app.use(express.json());

app.post('/login', (req, res) => {
    console.log(`login - user '${req.body.username}'`);

    res.status(200);
});

app.post('/logout', (req, res) => {
    console.log(`logout - user '${req.body.username}'`);

    res.status(200);
});

app.get('/index', (req, res) => {
    console.log('index');
    
    res.status(200);
});

app.get('/profile', (req, res) => {
    console.log('profile');
    
    res.status(200);
});

app.listen(3000, () => console.log('Example app listening on port 3000!'));