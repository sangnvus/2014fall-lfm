/***********************************************
* Cool DHTML tooltip script II- © Dynamic Drive DHTML code library (www.dynamicdrive.com)
* This notice MUST stay intact for legal use
* Visit Dynamic Drive at http://www.dynamicdrive.com/ for full source code
***********************************************/
var matched, browser;

jQuery.uaMatch = function (ua) {
    ua = ua.toLowerCase();

    var match = /(chrome)[ \/]([\w.]+)/.exec(ua) ||
        /(webkit)[ \/]([\w.]+)/.exec(ua) ||
        /(opera)(?:.*version|)[ \/]([\w.]+)/.exec(ua) ||
        /(msie) ([\w.]+)/.exec(ua) ||
        ua.indexOf("compatible") < 0 && /(mozilla)(?:.*? rv:([\w.]+)|)/.exec(ua) ||
        [];

    return {
        browser: match[1] || "",
        version: match[2] || "0"
    };
};

matched = jQuery.uaMatch(navigator.userAgent);
browser = {};

if (matched.browser) {
    browser[matched.browser] = true;
    browser.version = matched.version;
}

// Chrome is Webkit, but Webkit is also Safari.
if (browser.chrome) {
    browser.webkit = true;
} else if (browser.webkit) {
    browser.safari = true;
}

jQuery.browser = browser;
var offsetfromcursorX = -188 //Customize x offset of tooltip
var offsetfromcursorY = -80 //Customize y offset of tooltip

var offsetdivfrompointerX = 10 //Customize x offset of tooltip DIV relative to pointer image
var offsetdivfrompointerY = 14 //Customize y offset of tooltip DIV relative to pointer image. Tip: Set it to (height_of_pointer_image-1).

document.write('<div id="dhtmltooltip"></div>') //write out tooltip DIV
//document.write('<img id="dhtmlpointer" src="res/images/opt/arrow_tooltip.gif">') //write out pointer image

var ie = document.all
var ns6 = document.getElementById && !document.all
var enabletip = false
if (ie || ns6)
    var tipobj = document.all ? document.all["dhtmltooltip"] : document.getElementById ? document.getElementById("dhtmltooltip") : ""

var pointerobj = document.all ? document.all["dhtmlpointer"] : document.getElementById ? document.getElementById("dhtmlpointer") : ""

function ietruebody() {
    return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body
}

function ddrivetip(thetext, thewidth, thecolor) {
    if (ns6 || ie) {
        if (typeof thewidth != "undefined") tipobj.style.width = thewidth + "px"
        if (typeof thecolor != "undefined" && thecolor != "") tipobj.style.backgroundColor = thecolor
        tipobj.style.zIndex = 1000
        tipobj.innerHTML = thetext
        enabletip = true
        return false
    }
}

function positiontip(e) {
    if (enabletip) {
        var nondefaultpos = false
        var curX = (ns6) ? e.pageX : event.clientX + ietruebody().scrollLeft;
        var curY = (ns6) ? e.pageY : event.clientY + ietruebody().scrollTop;
        //Find out how close the mouse is to the corner of the window
        var winwidth = ie && !window.opera ? ietruebody().clientWidth : window.innerWidth - 20
        var winheight = ie && !window.opera ? ietruebody().clientHeight : window.innerHeight - 20

        var rightedge = ie && !window.opera ? winwidth - event.clientX - offsetfromcursorX : winwidth - e.clientX - offsetfromcursorX
        var bottomedge = ie && !window.opera ? winheight - event.clientY - offsetfromcursorY : winheight - e.clientY - offsetfromcursorY

        var leftedge = (offsetfromcursorX < 0) ? offsetfromcursorX * (-1) : -1000

        //if the horizontal distance isn't enough to accomodate the width of the context menu
        if (rightedge < tipobj.offsetWidth) {
            //move the horizontal position of the menu to the left by it's width
            tipobj.style.left = curX - tipobj.offsetWidth + "px"
            nondefaultpos = true
        }
        else if (curX < leftedge)
            tipobj.style.left = "5px"
        else {
            //position the horizontal position of the menu where the mouse is positioned
            tipobj.style.left = curX + offsetfromcursorX - offsetdivfrompointerX + "px"
            pointerobj.style.left = curX + offsetfromcursorX + "px"
        }

        //same concept with the vertical position
        if (bottomedge < tipobj.offsetHeight) {
            tipobj.style.top = curY - tipobj.offsetHeight - offsetfromcursorY + "px"
            nondefaultpos = true
        }
        else {
            tipobj.style.top = curY + offsetfromcursorY + offsetdivfrompointerY + "px"
            pointerobj.style.top = curY + offsetfromcursorY + "px"
        }
        tipobj.style.visibility = "visible"
        if (!nondefaultpos)
            pointerobj.style.visibility = "visible"
        else
            pointerobj.style.visibility = "hidden"
    }
}

function hideddrivetip() {
    if (ns6 || ie) {
        enabletip = false
        tipobj.style.visibility = "hidden"
        pointerobj.style.visibility = "hidden"
        tipobj.style.left = "-1000px"
        tipobj.style.backgroundColor = ''
        tipobj.style.width = ''
    }
}


//document.onmousemove = positiontip

$(document).ready(function () {
    $("#searchResult").mousemove(function (e) {

        if (enabletip) {
            var nondefaultpos = false
            var curX = (ns6) ? e.pageX : event.clientX + ietruebody().scrollLeft;
            var curY = (ns6) ? e.pageY : event.clientY + ietruebody().scrollTop;
            //Find out how close the mouse is to the corner of the window
            var winwidth = ie && !window.opera ? ietruebody().clientWidth : $(window).width()
            var winheight = ie && !window.opera ? ietruebody().clientHeight : $(window).height()

            var rightedge = ie && !window.opera ? winwidth - event.clientX - offsetfromcursorX : winwidth - e.clientX - offsetfromcursorX
            var bottomedge = ie && !window.opera ? winheight - event.clientY - offsetfromcursorY : winheight - e.clientY - offsetfromcursorY

            var leftedge = (offsetfromcursorX < 0) ? offsetfromcursorX * (-1) : -1000

            //if the horizontal distance isn't enough to accomodate the width of the context menu
            if (rightedge < tipobj.offsetWidth) {
                //move the horizontal position of the menu to the left by it's width
                tipobj.style.left = curX - tipobj.offsetWidth + "px"
                nondefaultpos = true
            }
            else if (curX < leftedge)
                tipobj.style.left = "5px"
            else {
                //position the horizontal position of the menu where the mouse is positioned
                tipobj.style.left = curX + offsetfromcursorX - offsetdivfrompointerX + "px"
                pointerobj.style.left = curX + offsetfromcursorX + "px"
            }

            //same concept with the vertical position
            if (bottomedge < tipobj.offsetHeight) {
                tipobj.style.top = curY - tipobj.offsetHeight - offsetfromcursorY + "px"
                nondefaultpos = true
            }
            else {
                tipobj.style.top = curY + offsetfromcursorY + offsetdivfrompointerY + "px"
                pointerobj.style.top = curY + offsetfromcursorY + "px"
            }
            tipobj.style.visibility = "visible"
            if (!nondefaultpos)
                pointerobj.style.visibility = "visible"
            else
                pointerobj.style.visibility = "hidden"
        }
    });

});

var msg_help_note = new Array();
//Da biet
msg_help_note[0] = '<div style="color:#F15A22"><b>Đã biết</b>: Văn bản này hiện Đã biết Ngày Hiệu lực,&nbsp; và Đã biết Tình trạng Hiệu lực;</div>'
//Tieng Anh
msg_help_note[1] = '<div style="color:#F15A22"><b>Tiếng Anh</b>:&nbsp; Văn bản Tiếng Việt được dịch ra Tiếng Anh;</div>'
//Van ban Goc
msg_help_note[2] = '<div style="color:#F15A22"><b>Văn bản gốc:</b>&nbsp; Văn bản được Scan từ bản gốc (Công báo), nó có giá trị pháp lý;</div>'
//LQ hieu luc
msg_help_note[3] = '<div style="color:#F15A22"><b>Liên quan hiệu lực</b>: Những Văn bản thay thế Văn bản này, hoặc bị Văn bản này thay thế, sửa đổi, bổ sung;</div>'
//Luoc do
msg_help_note[4] = '<div style="color:#F15A22"><b>Lược Đồ</b>: Giúp Bạn có được &quot;Gia Phả&quot; của Văn bản này với toàn bộ Văn bản liên quan;</div>'


var msg_help = new Array();
msg_help[0] = 'Các văn bản vừa mới ban hành.'
msg_help[1] = 'Chức năng tra cứu văn bản pháp luật mở rộng (không bao gồm công văn).'
msg_help[2] = 'Tra cứu theo tình trạng hiệu lực của văn bản: <b>Còn/Hết/Sắp hết</b> hiệu lực. (Dành cho Thành Viên Basic & Pro)'
msg_help[3] = 'Tra cứu theo thời điểm áp dụng của văn bản: <b>Sắp có/Vừa có/Chưa xác định</b> hiệu lực. (Dành cho Thành Viên Basic & Pro)'
msg_help[4] = 'Tra cứu văn bản theo <b>lĩnh vực, ngành nghề</b>. (Dành cho Thành Viên Basic & Pro)'
msg_help[5] = 'Tra cứu văn bản theo năm ban hành. (Dành cho Thành Viên <b><span style="color:#FF0000">B</span>a<span style="color:#FF0000">s</span>ic</b> và Thành Viên <b><span style="color:#FF0000">L</span>aw<span style="color:#FF0000">S</span>oft</b> <span style="color:#FF0000">Pro</span>)'
msg_help[6] = 'Tra cứu công văn.'
msg_help[7] = 'Tra cứu biểu thuế nhập khẩu WTO.'
msg_help[8] = 'Tra cứu bảng giá đất Tỉnh/Thành.'
msg_help[9] = ''
msg_help[10] = ''
msg_help[11] = ''





msg_help[12] = '350Tiện ích này chỉ Thành Viên <b style="font-family:Times New Roman; font-size:12px"><span style="color:#FF0000">B</span>a<span style="color:#FF0000">s</span>ic</b> và <b style="font-family:Times New Roman;font-size:12px"><span style="color:#FF0000">L</span>aw<span style="color:#FF0000">S</span>oft</b> <span style="color:#FF0000">Pro</span> mới xem được;</b><div class="px5"></div>Bạn Chuyển đổi Thành Viên tại ô Thành Viên phía trên, bên phải;<div class="px5"></div><b>Liên hệ:</b> (08) 3930 3279 _ 0906. 22 99 66<div class="px10"></div>'

msg_help[13] = '350Đây là tiện ích của Thành Viên <b style="font-family:Times New Roman; font-size:12px"><span style="color:#FF0000">B</span>a<span style="color:#FF0000">s</span>ic</b> và <b style="font-family:Times New Roman; font-size:12px"><span style="color:#FF0000">L</span>aw<span style="color:#FF0000">S</span>oft <span style="color:#FF0000">Pro</span><div class="px5"></div></b>Mời Đăng Nhập hoặc Đăng ký mới tại ô Thành Viên, phía trên, bên phải;<div class="px5"></div><b>Liên hệ:</b> (08) 3930. 3279 _ 0906. 22 99 66<div class="px10"></div>'



msg_help[14] = '350Đây là tiện ích của Thành Viên <b style="font-family:Times New Roman; font-size:12px"><span style="color:#FF0000">B</span>a<span style="color:#FF0000">s</span>ic</b> và <b style="font-family:Times New Roman; font-size:12px"><span style="color:#FF0000">L</span>aw<span style="color:#FF0000">S</span>oft <span style="color:#FF0000">Pro</span><div class="px5"></div></b>Mời Đăng Nhập hoặc Đăng ký mới tại ô Thành Viên, phía trên, bên phải;<div class="px5"></div><b>Liên hệ:</b> (08) 3930. 3279 _ 0906. 22 99 66<div class="px10"></div>'




msg_help[15] = 'Thông tin cá nhân và thời hạn sử dụng.'
msg_help[16] = 'Chuyển đổi Tiện ích tra cứu theo Thành Viên <b>Free</b> sang Thành Viên <b>Basic</b> hoặc <b>Pro</b>.'
msg_help[17] = 'Chuyển đổi Tiện ích tra cứu theo Thành Viên <b>Basic</b> sang Thành Viên <b>Pro</b>.'
msg_help[18] = 'Liệt kê các văn bản mà Bạn đã lưu trữ riêng.'
msg_help[19] = 'Đăng ký <b>Gia hạn</b> thời gian sử dụng.'
msg_help[20] = '450Chức năng này sẽ được cung cấp trong thời gian tới.'
msg_help[21] = 'Chuyển đổi Tiện ích tra cứu theo Thành Viên <b>Pro</b> sang Thành Viên <b>Basic</b>.'
msg_help[22] = '450Đây là nội dung có thu phí. Để xem Bạn cần phải đăng ký Thành Viên <b>Pro</b>.<br>Bạn <b>Chuyển đổi Thành Viên</b> tại ô <b>Thành Viên</b> phía trên hoặc<br>Liên hệ <b>(08) 3930 3279</b>.'
msg_help[23] = '450Đây là nội dung có thu phí. Để xem Bạn cần phải đăng ký Thành Viên <b><span style="color:#FF0000">L</span>aw<span style="color:#FF0000">S</span>oft</b> <span style="color:#FF0000">Pro</span>.<br>Hãy <b>Đăng nhập</b> hoặc <b>Đăng ký mới</b> tại ô <b>Thành Viên</b> phía trên hoặc<br>Liên hệ <b>(08) 3930 3279</b>.'
msg_help[24] = '450Bạn phải là Thành Viên mới xem được mục này.'
msg_help[25] = 'Tra cứu văn bản gốc.'
msg_help[26] = 'Tra cứu văn bản Tiếng Anh.'
msg_help[27] = '450Vì bạn là Thành Viên chưa kích hoạt nên chỉ được xem các văn bản ban hành trước 01/01/2008.'

msg_help[28] = '320Tiện ích dành  cho Thành Viên <b style="font-family:Times New Roman; font-size:13px"><span style="color:#FF0000">B</span>a<span style="color:#FF0000">s</span>ic</b> và Thành Viên <b style="font-family:Times New Roman;font-size:13px"><span style="color:#FF0000">L</span>aw<span style="color:#FF0000">S</span>oft</b> <span style="color:#FF0000">Pro</span>'
msg_help[29] = '300Bạn phải đăng nhập <b>Thành Viên</b> mới tải được Văn bản'
msg_help[30] = '300Tiện ích dành riêng cho Thành Viên <b style="font-family:Times New Roman;font-size:13px"><span style="color:#FF0000">L</span>aw<span style="color:#FF0000">S</span>oft</b> <span style="color:#FF0000">Pro</span>'


msg_help[31] = '350Đây là tiện ích của Thành Viên <b style="font-family:Times New Roman; font-size:12px"><span style="color:#FF0000">L</span>aw<span style="color:#FF0000">S</span>oft <span style="color:#FF0000">Pro</span><div class="px5"></div></b>Mời Đăng Nhập hoặc Đăng ký mới tại ô Thành Viên, phía trên, bên phải;<div class="px5"></div><b>Liên hệ:</b> (08) 3930. 3279 _ 0906. 22 99 66<div class="px10"></div>'








function LS_Tip_New(code, w, type) {

    var s = "";
    if (code == 13 && type == 1)
        s = msg_help[31] + msg_help_note[type];
    else
        s = msg_help[code] + msg_help_note[type];

    if (w == null) w = 180;
    if (w == 0) {
        ddrivetip(s.substr(3), s.substr(0, 3));
    }
    else ddrivetip(msg_help[code], w);
}

function LS_Tip_Type(type) {
    var s = msg_help_note[type];
    ddrivetip(s, 300);
}


function LS_Tip(code, w) {
    if (w == null) w = 180;
    if (w == 0) {
        var s = msg_help[code];
        ddrivetip(s.substr(3), s.substr(0, 3));
    }
    else ddrivetip(msg_help[code], w);
}

function LS_Hide() { hideddrivetip(); }

function LS_Tip_Type_Diagram(dgid) {
    var s = $(dgid).html();
    ddrivetip(s, 300);
}


var __temp = false;

function LS_Tootip_Type_Bookmark(dgid) {
    if (__temp == false) {
        View_NDSD(MemberGA);
        __temp = true;
    }


    var s = $(dgid).html().replace(new RegExp("</div>", "g"), "</div><br/>");
    ddrivetip(s, 400);
}

TB_WIDTH = 700;
TB_HEIGHT = 350;

function LS_Tip_Type_Bookmark(dgid) {
    View_NDSD(MemberGA);
    $("#divltrLienQuanHieuLucTungPhanDialog .child").html($(dgid).html());

    $("#divltrLienQuanHieuLucTungPhanDialog").dialog("open");
}


function LS_Tip_Type_Bookmark__(dgid) {
    hideddrivetip();
    $("body").append("<div id='TB_overlay'></div><div id='TB_window'></div>");
    $("#TB_overlay").click(TB_remove);
    $(window).resize(TB_position);
    $(window).scroll(TB_position);
    $("#TB_overlay").show();

    var imgPreloader = new Image();
    imgPreloader.onload = function () {
        var pagesize = getPageSize();
        $("#TB_window").append("<div id='TB_closeWindow'><a href='#' id='TB_closeWindowButton'><b>Đóng lại</b></a></div>");
        $("#TB_window").append("<div class='child'>" + $(dgid).html() + "</div>");

        $("#TB_closeWindowButton").click(TB_remove);
        TB_position();
        $("#TB_window").slideDown("normal");
    }
    imgPreloader.src = "http://tthc.thuvienphapluat.vn/images/logo.png";
}
function TB_remove() {
    $('#TB_window,#TB_overlay').remove();
    return false;
}

$(document).ready(function () {
    $(document).keyup(function (e) {
        if (e.keyCode == 27) { TB_remove(); }   // esc
    });
});

function TB_position() {
    var pagesize = getPageSize();

    if (window.innerHeight && window.scrollMaxY) {
        yScroll = window.innerHeight + window.scrollMaxY;
    } else if (document.body.scrollHeight > document.body.offsetHeight) { // all but Explorer Mac
        yScroll = document.body.scrollHeight;
    } else { // Explorer Mac...would also work in Explorer 6 Strict, Mozilla and Safari
        yScroll = document.body.offsetHeight;
    }

    var arrayPageScroll = getPageScrollTop();

    $("#TB_window").css({
        width: TB_WIDTH + "px", height: TB_HEIGHT + "px",
        left: ((pagesize[0] - TB_WIDTH) / 2 + 100) + "px", top: (arrayPageScroll[1] + ((pagesize[1] - TB_HEIGHT) / 2)) + "px"
    });
    $("#TB_overlay").css("height", yScroll + "px");

}
function getPageSize() {
    var de = document.documentElement;
    var w = window.innerWidth || self.innerWidth || (de && de.clientWidth) || document.body.clientWidth;
    var h = window.innerHeight || self.innerHeight || (de && de.clientHeight) || document.body.clientHeight;

    arrayPageSize = new Array(w, h)
    return arrayPageSize;
}
function getPageScrollTop() {
    var yScrolltop;
    if (self.pageYOffset) {
        yScrolltop = self.pageYOffset;
    } else if (document.documentElement && document.documentElement.scrollTop) {	 // Explorer 6 Strict
        yScrolltop = document.documentElement.scrollTop;
    } else if (document.body) {// all other Explorers
        yScrolltop = document.body.scrollTop;
    }
    arrayPageScroll = new Array('', yScrolltop)
    return arrayPageScroll;
}