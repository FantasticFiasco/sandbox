const express = require('express');
const app = express();

app.use(express.json());
app.use(validate);

app.post('/search', (req, res) => {
    console.log(`search - keyword '${req.body.kw}'`);

    res.status(200).json({
        results: [
            { id: 1, name: 'blue shirt' },
            { id: 2, name: 'red shirt' }
        ]
    });
});

app.get('/details/:id', (req, res) => {
    console.log(`details - product with id '${req.params.id}'`);
    
    res.status(200).json({ id: req.params.id });
});

app.post('/cart', (req, res) => {
    console.log(`cart - add product with id '${req.body.productId}'`);

    res.status(201).json({ id: req.body.productId });
});

app.listen(3000, () => console.log('Example app listening on port 3000!'));

function validate(req, res, next) {
    if (req.method === 'POST' && req.header('content-type') !== 'application/json') {
        res.sendStatus(415);
    } else {
        next();
    }
}