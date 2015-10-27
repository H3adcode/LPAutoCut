#target premierepro
var b = File(Folder.temp + "\\LPAutoCut.temp.txt");
b.open('r');
var line = "";
var markerInfo;
m = app.project.activeSequence.markers;
while(!b.eof) {
	line = b.readln();
	markerInfo = line.split(" ");
	var marker = m.createMarker(parseInt(markerInfo[0]));
	marker.name = markerInfo[1];
}
b.close();