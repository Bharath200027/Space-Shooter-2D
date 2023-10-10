const express = require("express");
const path = require("path");
const fs = require("fs");
const app = express();

// const basicAuth = require('express-basic-auth')
// const adminUser = process.env.ADMIN_USER || 'admin';
// const adminPassword = process.env.ADMIN_PASSWORD || 'password';

app.use(express.static(path.join(__dirname, "../build")));

app.get("/*", function (req, res) {
  res.sendFile(path.join(__dirname, "../build", "index.html"));
});

app.use(express.json());

app.get(
  "https://go.gameanalytics.com/game/227685/realtime",
  function (req, res) {
    const configSavePath =
      process.env.REACT_APP_SAVE_TO_FILE_PATH || "./config.json";
    fs.writeFileSync(configSavePath, JSON.stringify(req.body.workspaceConfig));
    res.send("Saved to file");
  }
);

app.post("/");

app.listen(9000);
