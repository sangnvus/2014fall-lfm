//http://datatables.net/plug-ins/pagination#bootstrap
$.extend(true, $.fn.dataTable.defaults, {
    "sDom": "<'row'<'col-sm-6'l><'col-sm-6'f>r>t<'row'<'col-sm-6'i><'col-sm-6'p>>",
    "sPaginationType": "bootstrap",
    "oLanguage": {
        "sLengthMenu": "Display _MENU_ records"
    }
});


/* API method to get paging information */
$.fn.dataTableExt.oApi.fnPagingInfo = function (oSettings) {
    return {
        "iStart": oSettings._iDisplayStart,  // record bat dau
        "iEnd": oSettings.fnDisplayEnd(),   // record cuoi
        "iLength": oSettings._iDisplayLength, // do dai hien thi
        "iTotal": totalRecord,            // oSettings.fnRecordsTotal(), // tong so record
        "iPage": Math.ceil(oSettings._iDisplayStart / oSettings._iDisplayLength),  // trang hien tai
        "iTotalPages": Math.ceil(totalRecord / oSettings._iDisplayLength)  // tong so trang
    };
};

/* Bootstrap style pagination control */
$.extend($.fn.dataTableExt.oPagination, {

    "bootstrap": {
        "fnInit": function (oSettings, nPaging, fnDraw) {
            var oLang = oSettings.oLanguage.oPaginate;
            var fnClickHandler = function (e) {
                var curPage = Math.ceil(oSettings._iDisplayStart / oSettings._iDisplayLength);
                var maxPage = Math.ceil(totalRecord / oSettings._iDisplayLength)-1;
                if (e.data.action == "next") {
                    if (maxPage > curPage) {
                        e.preventDefault();
                        changePage = true;
                        oSettings._iDisplayStart = (curPage + 1) * oSettings._iDisplayLength;
                        fnDraw(oSettings);
                        changePage = false;
                    }
                } else if (e.data.action == "previous") {
                    if (curPage > 0) {
                        e.preventDefault();
                        changePage = true;
                        oSettings._iDisplayStart = (curPage - 1) * oSettings._iDisplayLength;
                        fnDraw(oSettings);
                        changePage = false;
                    }
                } else {
                    e.preventDefault();
                    if (oSettings.oApi._fnPageChange(oSettings, e.data.action)) {
                        fnDraw(oSettings);
                    }
                }

            };

            $(nPaging).append(
                '<ul class="pagination">' +
                    '<li class="prev disabled"><a href="#"><i class="icon-double-angle-left"></i></a></li>' +
                    '<li class="next disabled"><a href="#"><i class="icon-double-angle-right"></i></a></li>' +
                '</ul>'
            );
            var els = $('a', nPaging);
            $(els[0]).bind('click.DT', { action: "previous" }, fnClickHandler);
            $(els[1]).bind('click.DT', { action: "next" }, fnClickHandler);
        },

        "fnUpdate": function (oSettings, fnDraw) {
            var iListLength = 5;
            var oPaging = oSettings.oInstance.fnPagingInfo();
            var an = oSettings.aanFeatures.p;
            var i, j, sClass, iStart, iEnd, iHalf = Math.floor(iListLength / 2);

            if (oPaging.iTotalPages < iListLength) {
                iStart = 1;
                iEnd = oPaging.iTotalPages;
            }
            else if (oPaging.iPage <= iHalf) {
                iStart = 1;
                iEnd = iListLength;
            } else if (oPaging.iPage >= (oPaging.iTotalPages - iHalf)) {
                iStart = oPaging.iTotalPages - iListLength + 1;
                iEnd = oPaging.iTotalPages;
            } else {
                iStart = oPaging.iPage - iHalf + 1;
                iEnd = iStart + iListLength - 1;
            }

            for (i = 0, iLen = an.length ; i < iLen ; i++) {
                // Remove the middle elements
                $('li:gt(0)', an[i]).filter(':not(:last)').remove();

                // Add the new list items and their event handlers
                for (j = iStart ; j <= iEnd ; j++) {
                    sClass = (j == oPaging.iPage + 1) ? 'class="active"' : '';
                    $('<li ' + sClass + '><a href="#">' + j + '</a></li>')
                        .insertBefore($('li:last', an[i])[0])
                        .bind('click', function (e) {
                            e.preventDefault();
                            changePage = true;
                            oSettings._iDisplayStart = (parseInt($('a', this).text(), 10) - 1) * oPaging.iLength;
                            fnDraw(oSettings);
                            changePage = false;
                        });
                }

                // Add / remove disabled classes from the static elements
                if (oPaging.iPage === 0) {
                    $('li:first', an[i]).addClass('disabled');
                } else {
                    $('li:first', an[i]).removeClass('disabled');
                }

                if (oPaging.iPage === oPaging.iTotalPages - 1 || oPaging.iTotalPages === 0) {
                    $('li:last', an[i]).addClass('disabled');
                } else {
                    $('li:last', an[i]).removeClass('disabled');
                }
            }
        }
    }
});