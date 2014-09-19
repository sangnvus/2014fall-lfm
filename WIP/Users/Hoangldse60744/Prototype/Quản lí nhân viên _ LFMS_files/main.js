function notification(title, text, timer, class_style) {
    if ($('.gritter-item-wrapper').length >= 3) {
        $('#gritter-notice-wrapper')[0].removeChild($(".gritter-item-wrapper")[0]);
    }
    var grit = $.gritter.add({
        title: title,
        text: text,
        sticky: false,
        time: timer,
        class_name: class_style
    });
}

function sortByKey(array, key) {
    return array.sort(function(a, b) {
        var x = a[key].split("/Date(").join("").split(")/").join("");
        var y = b[key].split("/Date(").join("").split(")/").join("");
        return ((Number(x) < Number(y)) ? -1 : ((Number(x) > Number(y)) ? 1 : 0));
    });
}

function convertNETDateTime(sNetDate) {
    if (sNetDate == null) return null;
    if (sNetDate instanceof Date) return sNetDate;
    var r = /\/Date\((-?\d+)\)\//i;
    var matches = sNetDate.match(r);
    if (matches.length == 2) {
        return new Date(parseInt(matches[1]));
    }
    else {
        return sNetDate;
    }
}

function getCurdate() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) { dd = "0" + dd } if (mm < 10) { mm = "0" + mm }
    today = dd + "/" + mm + "/" + yyyy;
    return today;
}


var delaySearch = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();