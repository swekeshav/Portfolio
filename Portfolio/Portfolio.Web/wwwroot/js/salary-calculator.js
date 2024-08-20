var salarySlider = document.getElementById('salarySlider');
var salaryInput = document.getElementById('salaryInput');
var salaryValueButtons = document.querySelectorAll('#salaryValues button');

salaryValueButtons.forEach(function (button) {
    button.addEventListener('click', function () {
        var newSalary = button.value * 100000;
        salarySlider.value = newSalary;
        salaryInput.value = newSalary;
        bifurcateSalary();
    });
});

salarySlider.addEventListener('input', salarySliderChanged);
salaryInput.addEventListener('input', salaryInputChanged);

function salarySliderChanged() {
    salaryInput.value = salarySlider.value;
    bifurcateSalary();
}

function salaryInputChanged() {
    salarySlider.value = salaryInput.value;
    bifurcateSalary();
}

bifurcateSalary();
function bifurcateSalary() {

    var grossYearly = Number(salarySlider.value);
    var grossMonthly = Math.round(grossYearly / 12);

    document.getElementById('grossYearly').textContent = grossYearly.toLocaleString('en-IN');
    document.getElementById('grossMonthly').textContent = grossMonthly.toLocaleString('en-IN');

    var basicYearly = Math.round(grossYearly / 2);
    var basicMonthly = Math.round(basicYearly / 12);
    document.getElementById('basicYearly').textContent = basicYearly.toLocaleString('en-IN');
    document.getElementById('basicMonthly').textContent = basicMonthly.toLocaleString('en-IN');

    var pfYearly = Math.round(basicYearly * 0.12);
    var pfMonthly = pfYearly / 12;
    document.getElementById('pfYearly').textContent = pfYearly.toLocaleString('en-IN');
    document.getElementById('pfMonthly').textContent = pfMonthly.toLocaleString('en-IN');

    var tdsYearly = calculateTDS(grossYearly);
    var tdsMonthly = Math.round(tdsYearly / 12);
    document.getElementById('tdsYearly').textContent = tdsYearly.toLocaleString('en-IN');
    document.getElementById('tdsMonthly').textContent = tdsMonthly.toLocaleString('en-IN');

    var incomeYearly = grossYearly - pfYearly - tdsYearly;
    var incomeMonthly = grossMonthly - pfMonthly - tdsMonthly;
    document.getElementById('incomeYearly').textContent = incomeYearly.toLocaleString('en-IN');
    document.getElementById('incomeMonthly').textContent = incomeMonthly.toLocaleString('en-IN');
}

function calculateTDS(salary) {
    var applicableTDS = 0;
    var fifteenLakhs = 1500000;
    var twelveLakhs = 1200000;
    var nineLakhs = 900000;
    var sixLakhs = 600000;
    var threeLakhs = 300000;

    var salary = salary - 50000; // Standard Deduction from FY23-24

    if (salary > fifteenLakhs) {
        applicableTDS = 150500 + (salary - fifteenLakhs) * 0.30;
    } else if (salary > twelveLakhs) {
        applicableTDS = 90000 + (salary - twelveLakhs) * 0.20;
    } else if (salary > nineLakhs) {
        applicableTDS = 45000 + (salary - nineLakhs) * 0.15;
    } else if (salary > sixLakhs) {
        applicableTDS = 15000 + (salary - sixLakhs) * 0.10;
    } else if (salary > threeLakhs) {
        applicableTDS = (salary - threeLakhs) * 0.05;
    }

    return applicableTDS;
}