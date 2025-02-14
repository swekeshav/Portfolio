const { test, expect } = require('@playwright/test');

test('Homework, login to website and read product Title', async ({ page }) => {
    const userId = 'neil@nitin.com';
    const pwd = 'Neil@123';

    await page.goto("https://rahulshettyacademy.com/client"); 

    await page.locator('#userEmail').fill(userId);
    await page.locator('#userPassword').fill(pwd);
    await page.locator('#login').click();

    const cardTitles = page.locator('.card .card-body h5');
    await cardTitles.last().waitFor();
    console.log(await cardTitles.allTextContents());
});