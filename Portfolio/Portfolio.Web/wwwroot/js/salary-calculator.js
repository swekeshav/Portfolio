$("#salaryRange").on('change', salaryRangeChanged);
$("#salaryInput").on('change', salaryInputChanged);

function salaryRangeChanged() {
    var newSalary = $("#salaryRange").val();
    $("#salaryInput").val(newSalary);
    bifurcateSalary();
}

function salaryInputChanged() {
    var newSalary = $("#salaryInput").val();
    $("#salaryRange").val(newSalary);
    bifurcateSalary();
}
//TODO: Convert this code to Javascript

bifurcateSalary();
function bifurcateSalary() {
    //TODO: Put this script in a different file
    var grossSalary = Number($("#salaryRange").val());
    var basicSalary = Math.round(grossSalary / 2);
    var oneSidedPF = Math.round(basicSalary * 0.12);
    var totalSalary = grossSalary + oneSidedPF;
    //TODO: Show P.A (Per Annum) and P.M (Per Month) salary details
    $("#baseSalary").text(basicSalary.toLocaleString('en-IN'));
    $("#grossSalary").text(grossSalary.toLocaleString('en-IN'));
    $("#employeePF").text(oneSidedPF.toLocaleString('en-IN'));
    $("#employerPF").text(oneSidedPF.toLocaleString('en-IN'));
    $("#totalSalary").text(totalSalary.toLocaleString('en-IN'));
}
