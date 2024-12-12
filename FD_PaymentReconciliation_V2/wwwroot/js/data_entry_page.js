$(document).ready(function () {

    $('#imageFullScreen').smartZoom({ 'containerClass': 'zoomableContainer' });
    $('#topPositionMap,#leftPositionMap,#rightPositionMap,#bottomPositionMap').bind("click", moveButtonClickHandler);
    $('#zoomInButton,#zoomOutButton').bind("click", zoomButtonClickHandler);
   
});

function zoomButtonClickHandler(e) {
    var scaleToAdd = 0.8;
    if (e.target.id == 'zoomOutButton')
        scaleToAdd = -scaleToAdd;
    $('#imageFullScreen').smartZoom('zoom', scaleToAdd);
}
function moveButtonClickHandler(e) {
    var pixelsToMoveOnX = 0;
    var pixelsToMoveOnY = 0;

    switch (e.target.id) {
        case "leftPositionMap":
            pixelsToMoveOnX = 50;
            break;
        case "rightPositionMap":
            pixelsToMoveOnX = -50;
            break;
        case "topPositionMap":
            pixelsToMoveOnY = 50;
            break;
        case "bottomPositionMap":
            pixelsToMoveOnY = -50;
            break;
    }
    $('#imageFullScreen').smartZoom('pan', pixelsToMoveOnX, pixelsToMoveOnY);
}

