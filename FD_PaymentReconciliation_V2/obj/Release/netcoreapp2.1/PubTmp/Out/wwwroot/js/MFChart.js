var myChart = null;

function Cumulative(intr, tenuer, fd_amt, min_amt, mat_val) {
    var amt_rec = ((fd_amt * mat_val) / min_amt);
    var total_interest = (amt_rec - fd_amt);
    var fdlabels = [], fddata = [];
    var fd_open_date = new Date();
    var reset_fd_open_date = new Date();
    var fd_mat_date = new Date();
    fd_mat_date.setMonth(fd_mat_date.getMonth() + parseInt(tenuer));
    var total_days = 0;
    var prev_int_acc = 0;

    var intr_per = (intr / 100.0);
    // seconds * minutes * hours * milliseconds = 1 day 
    var day = 60 * 60 * 24 * 1000;

    while (reset_fd_open_date <= fd_mat_date) {
        total_days = 0;
        var fd_fy_enddate = new Date('03/31/' + (reset_fd_open_date.getFullYear() + 1));

        if (fd_mat_date <= fd_fy_enddate) {
            total_days = date_diff_indays(reset_fd_open_date, fd_mat_date);
        } else {
            total_days = date_diff_indays(fd_open_date, fd_fy_enddate) + 1;
        }

        if (CheckLeapYear(reset_fd_open_date.getFullYear())) {
            total_days = total_days - 1;
        }

        var comp_per_year = parseInt((total_days / 365.0));

        var int_comp_year = fd_amt * Math.pow((1 + intr_per), comp_per_year);

        var broken_period = Math.round((total_days - (comp_per_year * 365)), 0);

        var intr_on_broken_period = Math.round(((int_comp_year * intr_per) / 365) * broken_period, 0);

        var total_int_acc = Math.round((int_comp_year - fd_amt + intr_on_broken_period), 0);

        var curr_fyy_intr = 0;

        if (fd_mat_date <= fd_fy_enddate) {
            total_int_acc = 0;
            $.each(fddata, function (i, n) {
                total_int_acc += n;
            });

            curr_fyy_intr = (amt_rec - fd_amt - total_int_acc);
        }
        else {
            curr_fyy_intr = total_int_acc - prev_int_acc;
            prev_int_acc = total_int_acc;
        }

        curr_fyy_intr = Math.round(curr_fyy_intr, 0);
        fddata.push(curr_fyy_intr);
        fdlabels.push(fd_fy_enddate.getFullYear().toString());
        reset_fd_open_date = new Date(fd_fy_enddate.getTime() + day);
    }
    LoadChart(fdlabels, fddata, 'Interest accumulated');
    $('#tdrtri').text(intr + ' %');
    var monthno = (fd_mat_date.getMonth() + 1).toString().length == 1 ? "0" + (fd_mat_date.getMonth() + 1) : (fd_mat_date.getMonth() + 1);
    var date = (fd_mat_date.getDate()).toString().length == 1 ? "0" + (fd_mat_date.getDate()) : (fd_mat_date.getDate());
    $('#tdmtrdate').text(date + "/" + monthno + "/" + fd_mat_date.getFullYear());
    $('#tdmtrvalue').text('₹ ' + Math.round(amt_rec, 0).toLocaleString('en-US'));
    $('#tdaggintamt').text('₹ ' + Math.round(total_interest, 0).toLocaleString('en-US'));
}

function NonCumulative_Yearly(intr, tenuer, fd_amt) {
    var isSkippedFirstYear = false;
    var addedamtintr = 0;
    var fdlabels = [], fddata = [];
    var fd_open_date = new Date();
    var res_fd_open_date = new Date();
    var fd_mat_date = new Date();
    var intr_rates = parseFloat((intr / 100));
    fd_mat_date.setMonth(fd_mat_date.getMonth() + parseInt(tenuer));
    var total_days = date_diff_indays(fd_open_date, fd_mat_date);
    var day = 60 * 60 * 24 * 1000;

    //Formula: ROUND(AMT OF DEPOSIT*RATE OF INTEREST%*PERIOD19/12,0)

    var MatAmt = 0;
    var total_interest = 0;
    total_interest = Math.round(fd_amt * intr_rates * tenuer / 12.0, 0);

    var daily_interest = (total_interest / total_days);
    var reset_fd_open_date = new Date();
    while (reset_fd_open_date <= fd_mat_date) {
        total_days = 0;
        var fd_fy_enddate = new Date('03/31/' + (reset_fd_open_date.getFullYear() + 1));

        if (fd_mat_date <= fd_fy_enddate) {
            total_days = date_diff_indays(reset_fd_open_date, fd_mat_date) + 1;
        } else {
            total_days = date_diff_indays(reset_fd_open_date, fd_fy_enddate) + 1;
        }

        var intramt = 0;

        if (total_days >= 365) {
            intramt = Math.round(((fd_amt * intr_rates)), 0);
        } else {
            intramt = Math.round(((fd_amt * intr_rates) / 365.0 * total_days), 0);
        }

        if (fd_mat_date <= fd_fy_enddate) {
            intramt = 0;
            var total_int_acc = 0;
            $.each(fddata, function (i, n) {
                total_int_acc += n;
            });
            intramt = Math.round((total_interest - total_int_acc), 0);
        }

        intramt = Math.round(intramt, 0);

        addedamtintr += intramt;

        fddata.push(intramt);
        fdlabels.push(fd_fy_enddate.getFullYear().toString());
        reset_fd_open_date = new Date(fd_fy_enddate.getTime() + day);
        isSkippedFirstYear = true;
    }

    LoadChart(fdlabels, fddata, 'Interest payable');

    $('#tdrtri').text(intr + ' %');
    var monthno = (fd_mat_date.getMonth() + 1).toString().length == 1 ? "0" + (fd_mat_date.getMonth() + 1) : (fd_mat_date.getMonth() + 1);
    var date = (fd_mat_date.getDate()).toString().length == 1 ? "0" + (fd_mat_date.getDate()) : (fd_mat_date.getDate());
    $('#tdmtrdate').text(date + "/" + monthno + "/" + fd_mat_date.getFullYear());
    //$('#tdmtrvalue').text('₹ ' + Math.round((fd_amt + addedamtintr), 2).toLocaleString('en-US'));
    $('#tdmtrvalue').text('₹ ' + Math.round((fd_amt), 2).toLocaleString('en-US'));
    //$('#tdaggintamt').text('₹ ' + Math.round((addedamtintr), 2).toLocaleString('en-US'));
    $('#tdaggintamt').text('₹ ' + Math.round((total_interest), 2).toLocaleString('en-US'));
}

function NonCumulative_Monthly(intr, tenuer, fd_amt) {
    var isSkippedFirstYear = false;
    var addedamtintr = 0;
    var fdlabels = [], fddata = [], fdYearlydata = [];
    var fd_open_date = new Date();
    var res_fd_open_date = new Date();
    var fd_mat_date = new Date();
    var intr_rates = parseFloat((intr / 100));
    fd_mat_date.setMonth(fd_mat_date.getMonth() + parseInt(tenuer));
    var total_days = date_diff_indays(fd_open_date, fd_mat_date);
    var day = 60 * 60 * 24 * 1000;

    //Formula: ROUND(AMT OF DEPOSIT*RATE OF INTEREST%*PERIOD19/12,0)

    var MatAmt = 0;
    var total_interest = 0;
    total_interest = Math.round(fd_amt * intr_rates * tenuer / 12.0, 0);

    var daily_interest = (total_interest / total_days);
    var reset_fd_open_date = new Date();
    while (reset_fd_open_date <= fd_mat_date) {
        total_days = 0;

        var fd_fy_enddate = new Date('03/31/' + (reset_fd_open_date.getFullYear() + 1));

        var total_months = 0;

        if (fd_mat_date <= fd_fy_enddate) {
            total_months = monthDiff(reset_fd_open_date, fd_mat_date);
        } else {
            total_months = monthDiff(reset_fd_open_date, fd_fy_enddate);
        }

        for (var month = 0; month <= total_months; month++) {

            var month_end_date = new Date(fd_open_date.getFullYear(), fd_open_date.getMonth() + 1, 0);

            total_days = date_diff_indays(fd_open_date, month_end_date) + 1;

            var month_days = month_end_date.getDate();

            if (total_days < month_days) {
                intramt = Math.round(((fd_amt * intr_rates) / 365.0 * total_days), 0);

            } else {
                intramt = Math.round(((fd_amt * intr_rates) / 12.0), 0);
            }

            fdYearlydata.push(intramt);

            fd_open_date = new Date(month_end_date.getTime() + day);
        }

        var total_int_acc = 0;
        $.each(fdYearlydata, function (i, n) {
            total_int_acc += n;
        });

        if (fd_mat_date <= fd_fy_enddate) {
            var prev_year_intr = 0;
            $.each(fddata, function (i, n) {
                prev_year_intr += n;
            });

            var lastmonthamt = fdYearlydata[fdYearlydata.length - 1];
            var newlastmonthamt = Math.round((total_interest - (prev_year_intr + (total_int_acc - lastmonthamt))), 0);
            fdYearlydata[fdYearlydata.length - 1] = newlastmonthamt;

            total_int_acc = 0;
            $.each(fdYearlydata, function (i, n) {
                total_int_acc += n;
            });
        }

        fdYearlydata = [];
        total_int_acc = Math.round(total_int_acc, 0);
        fddata.push(total_int_acc);
        fdlabels.push(fd_fy_enddate.getFullYear().toString());
        reset_fd_open_date = new Date(fd_fy_enddate.getTime() + day);
        isSkippedFirstYear = true;
    }

    LoadChart(fdlabels, fddata, 'Interest payable');

    $('#tdrtri').text(intr + ' %');
    var monthno = (fd_mat_date.getMonth() + 1).toString().length == 1 ? "0" + (fd_mat_date.getMonth() + 1) : (fd_mat_date.getMonth() + 1);
    var date = (fd_mat_date.getDate()).toString().length == 1 ? "0" + (fd_mat_date.getDate()) : (fd_mat_date.getDate());
    $('#tdmtrdate').text(date + "/" + monthno + "/" + fd_mat_date.getFullYear());
    //$('#tdmtrvalue').text('₹ ' + Math.round((fd_amt + addedamtintr), 2).toLocaleString('en-US'));
    $('#tdmtrvalue').text('₹ ' + Math.round((fd_amt), 2).toLocaleString('en-US'));
    //$('#tdaggintamt').text('₹ ' + Math.round((addedamtintr), 2).toLocaleString('en-US'));
    $('#tdaggintamt').text('₹ ' + Math.round((total_interest), 2).toLocaleString('en-US'));
}

var date_diff_indays = function (date1, date2) {
    dt1 = new Date(date1);
    dt2 = new Date(date2);
    return Math.floor((Date.UTC(dt2.getFullYear(), dt2.getMonth(), dt2.getDate()) - Date.UTC(dt1.getFullYear(), dt1.getMonth(), dt1.getDate())) / (1000 * 60 * 60 * 24));
}

function monthDiff(d1, d2) {
    var months;
    months = (d2.getFullYear() - d1.getFullYear()) * 12;
    months -= d1.getMonth() + 1;
    months += d2.getMonth() + 1;
    return months <= 0 ? 0 : months;
}

function LoadChart(fdlabels, fddata, yaxisname) {
    if (myChart != null) {
        myChart.destroy();
    }
    var ctx = $('#barChart').get(0).getContext('2d');
    myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: fdlabels,
            datasets: [{
                label: '',
                data: fddata,
                backgroundColor: "#e51937",
                borderWidth: 1
            }]
        },
        options: {
            legend: { display: false },
            tooltips: {
                callbacks: {
                    label: function (tooltipItem) {
                        return '₹ ' + tooltipItem.yLabel.toLocaleString('en-US');
                    }
                }
            },
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                yAxes: [{
                    gridLines: {
                        display: false
                    },
                    scaleLabel: {
                        display: true,
                        labelString: yaxisname//'Interest Accumulated'
                    },
                    //stacked: true,
                    ticks: {
                        beginAtZero: true,
                        //stepSize: 1000,
                        callback: function (value, index, values) {
                            return '₹ ' + value.toLocaleString('en-US');
                        }
                    }
                }],
                xAxes: [{
                    gridLines: {
                        display: false
                    },
                    scaleLabel: {
                        display: true,
                        labelString: 'Financial year'//'Interest Amount'
                    },
                    // Change here
                    //barThickness: 45,
                    barPercentage: 0.8,
                    categoryPercentage: 0.1,
                    stacked: true,
                    ticks: {
                        callback: function (value, index, values) {
                            return 'FY-' + value;
                        }
                    }
                }]
            }
        }
    });

    $('#tbodygraph').html('');
    var graphtable = '';
    $.each(fdlabels, function (i, r) {
        graphtable += '<tr>';
        graphtable += '<td>';
        graphtable += ((r - 1) + '-' + r);
        graphtable += '</td>';
        graphtable += '<td>';
        graphtable += '₹ ' + fddata[i].toLocaleString('en-US');
        graphtable += '</td>';
        graphtable += '</tr>'
    });
    $('#tbodygraph').html(graphtable);
}

function CheckLeapYear(year) {
    return (year % 100 === 0) ? (year % 400 === 0) : (year % 4 === 0);
}
