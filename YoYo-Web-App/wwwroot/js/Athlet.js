
var count;
var timerElement;
var timerIntervalObj;
var cdTimerIntervalObj;
var seconds;
var minutes;
var currentTime;

// CountDown Timer
var cdtSeconds;
var cdtMinutes;
var cdTimerElement;
var cdtCurrentTime;
var countDown;


var fitnessScoreData;
var allAthletssData;

var progressBarObj = { start: 0, end: 0, current: 0 }

var currentShuttleLevelNumber;
var currentShuttleNumber;
var currentShuttleSpeed;
var currentTotalDistance;



$(function () {
    init();
});

function init() {
    count = 0;
    timerElement = $('#timer');
    cdTimerElement = $('#cdTimer');
    currentShuttleNumber = 0;
    currentTime = "00:00";

    getFitnessScoreData();
    getAthletData();
    $("#controls").removeAttr("hidden");
    $("#pauseBtn").hide();
    $("#playBtn").show();
    $("#restartBtn").hide();


}

function fitnessScoreDataLoaded() {
    initProgressBarData();
}


function stop() {

    stopTimer();

    $("#pauseBtn").hide();
    $("#playBtn").show();
}

function start() {
    startTimer();
    startCountDownTimer();

    processSuttle();

    $("#pauseBtn").show();
    $("#playBtn").hide();
    $(".btnAthlet").removeAttr("hidden");
    $("#finishTestBtn").removeAttr("hidden");

}

function restart() {
    location.reload();
}

// Timer Logic..
function timer() {
    count++;

    seconds = pad(count % 60);
    minutes = pad(parseInt(count / 60));

    currentTime = minutes + ":" + seconds

    timerElement.text(currentTime);
    processSuttle();

}


function countDownTimer() {
    countDown--;

    cdtSeconds = pad(countDown % 60);
    cdtMinutes = pad(parseInt(countDown / 60));

    cdtCurrentTime = cdtMinutes + ":" + cdtSeconds

    cdTimerElement.text(cdtCurrentTime);

    if (countDown <= 0) {
        clearInterval(cdTimerIntervalObj);
        countDown = 0;
    }

}
 
function pad(val) {
    let valString = val + "";
    if (valString.length < 2) {
        return "0" + valString;
    } else {
        return valString;
    }
}

function startTimer() {
    timerIntervalObj = setInterval(timer, 1000);
}

function startCountDownTimer() {
    cdTimerIntervalObj = setInterval(countDownTimer, 1000);
}

function stopTimer() {
    clearInterval(timerIntervalObj);
    clearInterval(cdTimerIntervalObj);
}

// Shuttle Processing..
function processSuttle() {
    if (fitnessScoreData) {
        $.each(fitnessScoreData, function (index, item) {

            if (item.startTime === currentTime) {

                currentShuttleLevelNumber = item.speedLevel;
                currentShuttleNumber = item.shuttleNo;
                currentShuttleSpeed = item.speed;
                if (index > -1) currentTotalDistance = item.accumulatedShuttleDistance;

                if (index < fitnessScoreData.length) {
                    if (index !== fitnessScoreData.length - 1) {
                        let countDownSeconds = calculateCountDownTime(fitnessScoreData[index], fitnessScoreData[index + 1]);
                        countDown = countDownSeconds;
                    }

                    if (cdTimerIntervalObj) {
                        clearInterval(cdTimerIntervalObj);
                    }

                    startCountDownTimer();

                     
                    progressBarObj.current = item.accumulatedShuttleDistance;
                     
                    calculateProgressBarData();
                }

                if (index === fitnessScoreData.length - 1) {
                    finishTest();
                }


                updateProcessedUi();
            }
        });
    }
}

function updateProcessedUi() {
    $("#currentShuttleLevelNumber").text(currentShuttleLevelNumber);
    $("#currentShuttleNumber").text(currentShuttleNumber);
    $("#currentShuttleSpeed").text(currentShuttleSpeed);
    $("#currentTotalDistance").text(currentTotalDistance);
}

function initProgressBarData() {
    if (fitnessScoreData) {
        progressBarObj.start = fitnessScoreData[0].accumulatedShuttleDistance;
        progressBarObj.end = fitnessScoreData[fitnessScoreData.length - 1].accumulatedShuttleDistance;
        progressBarObj.current = fitnessScoreData[0].accumulatedShuttleDistance;
    }

    animateElements();
}

function animateElements() {
    $('.progressbar').each(function () {
        var elementPos = $(this).offset().top;
        var topOfWindow = $(window).scrollTop();
        var percent = $(this).find('.circle').attr('data-percent');
        var percentage = parseInt(percent, 10) / parseInt(100, 10);
        var animate = $(this).data('animate');
        if (elementPos < topOfWindow + $(window).height() - 30 && !animate) {
            $(this).data('animate', true);
            $(this).find('.circle').circleProgress({
                startAngle: -Math.PI / 2,
                value: percent / 100,
                thickness: 14,
                fill: {
                    color: '#1B58B8'
                }
            }).on('circle-animation-progress', function (event, progress, stepValue) {
                $(this).find('div').text((stepValue * 100).toFixed(1) + "%");
            }).stop();
        }
    });
}

function getFitnessScoreData() {
    $.ajax({
        url: "/AthletProgress/GetFitnessScoreData", success: function (result) {
            fitnessScoreData = result;
            fitnessScoreDataLoaded();
            
        }
    });
}

function getAthletData() {
    $.ajax({
        url: "/AthletProgress/GetAthlets", success: function (result) {
            allAthletssData = result;
        }
    });
}

function calculateCountDownTime(currentRating, nextRating) {

    let currentTime = currentRating.startTime.split(":");
    let nextTime = nextRating.startTime.split(":");

    let countDownTime = (parseInt(nextTime[0]) * 60 + parseInt(nextTime[1])) - (parseInt(currentTime[0]) * 60 + parseInt(currentTime[1]));

    return countDownTime;
}

function calculateProgressBarData() {
    var progressPercent = percentage(parseInt(progressBarObj.current), parseInt(progressBarObj.end));
    $("#speed-level-progress-bar").css('width', progressPercent + '%');
}

function percentage(partialValue, totalValue) {
    return parseInt((100 * partialValue) / totalValue);
}


function warnAthlet(athletIdUiRef, athletId) {
    let btnName = '#warnBtn' + athletIdUiRef;
    let stopBtnName = '#stopBtn' + athletIdUiRef;

    $(btnName).text('Warned');
    $(btnName).attr("disabled", true);
    $(btnName).removeClass("btn-outline-warning");
    $(btnName).addClass("btn-outline-dark");

    $(stopBtnName).attr("disabled", false);

}

function stopAthlet(athletIdUiRef, athletId) {

    let finishedAthlet = '.finished' + athletIdUiRef;
    let finishedAthletDd = '#finished' + athletIdUiRef;
    let btnAthlet = '.btn' + athletIdUiRef;

    $(btnAthlet).hide();

    $(finishedAthlet).removeAttr("hidden");
    populateDropDown(finishedAthletDd, currentShuttleLevelNumber, currentShuttleNumber);

    let athletResult = currentShuttleLevelNumber + "-" + currentShuttleNumber;

    setAthletResult(athletId, athletResult);
}

function populateDropDown(playerIdentifier, currentShuttleLevelNumber, currentShuttleNumber) {
    $(function () {
        let ddPlayerResult = $(playerIdentifier);

        let option1 = $("<option />");
        option1.html("Choose");
        option1.val("");
        ddPlayerResult.append(option1);

        //Loop and add the Year values to DropDownList.
        $.each(fitnessScoreData, function (index, item) {
            let option = $("<option />");
            option.html(item.speedLevel + "-" + item.shuttleNo);
            option.val(item.speedLevel + "-" + item.shuttleNo);
            if (item.speedLevel === currentShuttleLevelNumber && item.shuttleNo === currentShuttleNumber) {
                option.attr("selected", true);
            }
            ddPlayerResult.append(option);
        });

    });
}

function athletResultChanged(athletId, athletElementRefId) {
    let athletRef = "#" + athletElementRefId;
    let athletResult = $(athletRef).val();
    setAthletResult(athletId, athletResult);

}

function setAthletResult(athletId, athletResult) {
    $.ajax({
        url: "/AthletProgress/AthletResult/" + athletId,
        type: "POST",
        data: {
            id: athletId,
            result: athletResult
        },
        success: function (result) {
        }
    });
}

function finishTest() {
    $.each(allAthletssData, function (index, item) {
        let runningAthlet = ".finishedAthlet-" + item.id;

        if ($(runningAthlet).attr("hidden")) {
            let nonfinishedAthlet = "Athlet-" + item.id;
            stopAthlet(nonfinishedAthlet, item.id);
        }

    })

    stop();

    $("#finishTestBtnEle").text("Finished");
    $("#pauseBtn").hide();
    $("#playBtn").hide();
    $("#restartBtn").show();
}
