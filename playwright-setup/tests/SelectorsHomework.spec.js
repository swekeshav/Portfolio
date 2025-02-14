const { test, expect } = require('@playwright/test');

test.only('Homework, login to website and read product Title', async ({ page }) => {
    const userId = 'neil@nitin.com';
    const pwd = 'Neil@123';

    await page.goto("https://rahulshettyacademy.com/client"); 

    const userName = page.getByRole('textbox', { name: 'email@example.com' });
    const password = page.getByRole('textbox', { name: 'enter your passsword' });
    const loginBtn = page.getByRole('button', { name: 'Login' });

    await userName.fill(userId);
    await password.fill(pwd);
    await loginBtn.click();

    console.log(await page.locator('.card .card-body h5').nth(0).textContent());
});