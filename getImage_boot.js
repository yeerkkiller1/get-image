var express = require("express");
var exec = require("exec");
var app = express();


var allowCrossDomain = function(req, res, next) {
    res.header('Access-Control-Allow-Origin', '*');
    res.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE');
    res.header('Access-Control-Allow-Headers', 'Content-Type');

    next();
}

app.use(allowCrossDomain);

function doRequest(url, res) {
	console.log("Requesting url", url);

	exec(["mono", "downloadBase64/bin/Release/downloadBase64.exe", url], function(error, stdout, stderror){
		error = error || stderror.toString();

		if(error) {
			console.error(error);
		}
		var image = stdout.toString();
		var data_uri_prefix = "data:image/x-icon;base64,";
		image = data_uri_prefix + image;
		if(res) {
			res.send(image);
        	//res.send('<img src="'+image+'"/>');
    	}
	});
}

app.get("*", function(req, res) {
	console.log("Got request", req.path);
	var url = unescape(req.path).slice(1) + "/favicon.ico";
	doRequest(url, res);
});

console.log("Before https setup");

var fs = require('fs');
var http = require('http');
var https = require('https');
var privateKey  = fs.readFileSync('../SiteWatcher/key.pem', 'utf8');
var certificate = fs.readFileSync('../SiteWatcher/cert.pem', 'utf8');

var credentials = {key: privateKey, cert: certificate};

//var httpServer = http.createServer(app);
var httpsServer = https.createServer(credentials, app);

console.log(typeof privateKey);

console.log("After https setup");

//httpServer.listen(5555);
httpsServer.listen(5555);
