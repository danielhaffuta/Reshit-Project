function genericPopup(href, width, height, scrollbars)
{
    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : window.screenX;
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : window.screenY;

    var screenWidth = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    var screenHeight = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

    var left = ((screenWidth / 2) - (width / 2)) + dualScreenLeft;
    var top = ((screenHeight / 2) - (height / 2)) + dualScreenTop;
    var param = "width=" + width + ", height=" + height + ", scrollbars=" + scrollbars + ", resizable, status,top = " + top + ",left = " + left ;

    return window.open(href, "", param);
}

var clickCount = 0;
var timeoutID = 0;

function OnClick(href, width, height, scrollbars)
{
    clickCount++;

    if (clickCount >= 2) {
        clickCount = 0;          //reset clickCount
        clearTimeout(timeoutID); //stop the single click callback from being called
        genericPopup(href, width, height, scrollbars);   //perform your double click action
    }
    else if (clickCount == 1) {
        //create a callback that will be called in a few miliseconds
        //the callback time determines how long the user has to click a second time

        var callBack = function () {
            //make sure this wasn't fired at
            //the same time they double clicked
            if (clickCount == 1) {
                clickCount = 0;         //reset the clickCount
                SingleClickFunction();  //perform your single click action
            }
        };

        //This will call the callBack function in 500 milliseconds (1/2 second).
        //If by that time they haven't clicked the LinkButton again
        //We will perform the single click action. You can adjust the
        //Time here to control how quickly the user has to double click.
        timeoutID = setTimeout(callBack, 500);
    }
}