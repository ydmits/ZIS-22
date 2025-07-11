const express = require("express");
const fs = require("fs");
const path = require("path");

const app = express();
const jsonParser = express.json();

const filePath = path.join(__dirname, "db.json");

app.use(express.static(path.join(__dirname, "public")));

app.get("/api/entries", (req, res) => {
    const content = fs.readFileSync(filePath, "utf8");
    const entries = JSON.parse(content);
    res.send(entries);
});

app.get("/api/entries/:id", (req, res) => {
    const id = parseInt(req.params.id, 10);
    const content = fs.readFileSync(filePath, "utf8");
    const entries = JSON.parse(content);
    const entry = entries.find(e => e.id === id);
    if (entry) {
        res.send(entry);
    } else {
        res.status(404).send();
    }
});

app.post("/api/entries", jsonParser, (req, res) => {
    if (!req.body) return res.sendStatus(400);

    const { name, city, address, dob, email } = req.body;
    const newEntry = {
        id: Date.now(),
        name,
        city,
        address,
        dob,
        email
    };

    const content = fs.readFileSync(filePath, "utf8");
    const entries = JSON.parse(content);
    entries.push(newEntry);
    fs.writeFileSync(filePath, JSON.stringify(entries));

    res.send(newEntry);
});

app.delete("/api/entries/:id", (req, res) => {
    const id = parseInt(req.params.id, 10);
    const content = fs.readFileSync(filePath, "utf8");
    const entries = JSON.parse(content);
    const newEntries = entries.filter(e => e.id !== id);
    fs.writeFileSync(filePath, JSON.stringify(newEntries));
    res.sendStatus(200);
});

const port = process.env.PORT || 3000;
app.listen(port, () => {
    console.log(`Server is running on port ${port}`);
});

// http://localhost:3000