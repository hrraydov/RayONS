$.noConflict();
jQuery(document).ready(function ($) {
    $(function () {
        $(".dialog").dialog({
            autoOpen: false,
            resizable: false,
            position: { my: "center top", at: "center top", of: window },
            minWidth: 400,
            minHeight: 600,
        });
        $("#categoryMenu").menu();
        $(".categoryTable").hide();
    });
    $(".submitButton").live("click", function () {
        myEditor.saveHTML();
    })
    $("#logOnLink").live("click", function () {
        $("#logOnDialog").dialog("open");
        $.ajax({
            url: "/Account/LogOn",
            success: function (data) {
                $("#logOnDialog").html($(data).find("#main").html());
            }
        });

    });
    $(".registerLink").live("click", function () {
        $("#registerDialog").dialog("open");
        $.ajax({
            url: "/Account/Register",
            success: function (data) {
                $("#registerDialog").html($(data).find("#main").html());
            }
        });

    });
    $(".subcategoryLink").live("click", function () {
        $.ajax({
            url: "/Lessons/ViewLessons/" + $(this).attr("data-subcategory-id") + "?page=1",
            success: function (data) {
                $("#content").html($(data).find("#main").html());
            }
        });
    });
    $(".lessonLink").live("click", function () {
        $.ajax({
            url: "/Lessons/ViewLesson/?url=Lesson" + $(this).attr("data-target") + "&LessonName=" + $(this).attr("data-lesson-name"),
            success: function (data) {
                $("#content").html($(data).find("#main").html());
            }
        });
    });
    $(".pageLink").live("click", function () {
        $.ajax({
            url: "/Lessons/ViewLessons/" + $(this).attr("data-subcategory-id") + "/?page=" + $(this).attr("data-page-number"),
            success: function (data) {
                $("#content").html($(data).find("#main").html());
            }
        });
    });
    $(".subcategoryLinkForTests").live("click", function () {
        $.ajax({
            url: "/Tests/ViewTests/" + $(this).attr("data-subcategory-id") + "?page=1",
            success: function (data) {
                $("#content").html($(data).find("#main").html());
            }
        });
    });
    $(".pageLinkForTest").live("click", function () {
        $.ajax({
            url: "/Tests/ViewTests/" + $(this).attr("data-subcategory-id") + "/?page=" + $(this).attr("data-page-number"),
            success: function (data) {
                $("#content").html($(data).find("#main").html());
            }
        });
    });
    $(".testLink").live("click", function () {
        $.ajax({
            url: "/Tests/ViewTest/" + $(this).attr("data-target"),
            success: function (data) {
                $("#content").html($(data).find("#main").html());
            }
        });
    });
    $(".categoryLinker").live("click", function () {
        var target = $(this).attr("data-target");
        if ($("#" + target).css("display") !== "none") {
            $("#" + target).hide();
        } else {
            $("#" + target).show();
        }
    });
});



