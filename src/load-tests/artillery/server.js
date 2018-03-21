const express = require('express');
const app = express();

app.use(express.json());

app.post('/search', (req, res) => {
    console.log(`search - keyword '${req.body.kw}'`);

    res.status(200).json([
        { id: 1, name: 'blue shirt' },
        { id: 2, name: 'red shirt' }
    ]);
});

app.get('/details/:id', (req, res) => {
    console.log(`details - product with id '${req.params.id}'`);
    
    res.status(200).json({});
});

app.post('/cart', (req, res) => {
    console.log(`cart - add product with id '${req.body.id}'`);

    res.status(201).json({ id: req.params.id });
});

app.listen(3000, () => console.log('Example app listening on port 3000!'));