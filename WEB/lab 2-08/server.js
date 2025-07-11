const express = require('express');
const app = express();
const port = 3001;

app.use(express.json());

let batteries = [
  { id: 1, name: 'Battery 1' },
  { id: 2, name: 'Battery 2' },
];

app.get('/api/batteries', (req, res) => {
  res.json(batteries);
});

app.get('/api/batteries/:id', (req, res) => {
  const battery = batteries.find(b => b.id === parseInt(req.params.id));
  if (!battery) return res.status(404).send('Battery not found');
  res.json(battery);
});

app.post('/api/batteries', (req, res) => {
  const battery = {
    id: batteries.length + 1,
    name: req.body.name,
  };
  batteries.push(battery);
  res.status(201).json(battery);
});

app.put('/api/batteries/:id', (req, res) => {
  const battery = batteries.find(b => b.id === parseInt(req.params.id));
  if (!battery) return res.status(404).send('Battery not found');

  battery.name = req.body.name;
  res.json(battery);
});

app.delete('/api/batteries/:id', (req, res) => {
  const batteryIndex = batteries.findIndex(b => b.id === parseInt(req.params.id));
  if (batteryIndex === -1) return res.status(404).send('Battery not found');

  const deletedBattery = batteries.splice(batteryIndex, 1);
  res.json(deletedBattery);
});

app.listen(port, () => {
  console.log(`Server is running on http://localhost:${port}`);
});
