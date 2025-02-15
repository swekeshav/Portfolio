const { test, expect } = require('@playwright/test');


test('Generate test using codegen', async ({ page }) => {
    await page.goto('https://google.com')

    await page.getByRole('combobox', { name: 'Search' }).fill('rahulshettyacademy');
    await page.getByRole('combobox', { name: 'Search' }).press('Enter');

    await page.locator("[href='https://rahulshettyacademy.com/']").click();
    await page.waitForLoadState('load');

    expect(page).toHaveURL('https://rahulshettyacademy.com/');
    await page.pause();
});