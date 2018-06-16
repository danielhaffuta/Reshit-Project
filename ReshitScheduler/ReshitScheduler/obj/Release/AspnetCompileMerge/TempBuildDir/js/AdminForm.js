$(document).ready(function ()
{
    $(".cross").hide();
    $(".menu").hide();
    $(".options").hide();

    $(".hamburger").click(function ()
    {
        $(".menu").css("visibility", "visible");

        $(".menu").slideToggle("slow", function ()
        {
            $(".hamburger").hide();
            $(".cross").show();
        });
    });

    $(".cross").click(function () {
        $(".menu").slideToggle("slow", function () {
            $(".cross").hide();
            $(".hamburger").show();
            $(".options").hide();
        });
    });

});

function form1()
{
    $("#allOptions").css("visibility", "visible");
    $(".options").hide();
    $("#editTables").show();

}
function classShow() {

    $(".options").hide();
    $("#classShow").show();
}
function disconnect() {
    window.location.replace("LoginForm.aspx");

}