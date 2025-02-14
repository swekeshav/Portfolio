const { test, expect } = require('@playwright/test');

test('Reading TextContent', async ({ page }) => {
    const userName = page.locator("#username");
    const signInBtn = page.locator("#signInBtn");

    await page.goto("https://rahulshettyacademy.com/loginpagePractise/");
    console.log(await page.title());

    await userName.fill("rahulshetty");
    await page.locator("#password").fill("learning");

    await signInBtn.click();

    await expect(page.locator("[style *= 'block']")).toContainText("Incorrect username/password.")
});

test('Iterating through Locators', async ({ page }) => {
    const userName = page.locator("#username");
    const signInBtn = page.locator("#signInBtn");
    const cardTitles = page.locator('.card-body .card-title');

    await page.goto("https://rahulshettyacademy.com/loginpagePractise/");
    console.log(await page.title());

    await userName.fill("rahulshettyacademy");
    await page.locator("#password").fill("learning");
    
    await signInBtn.click();

    
    // This will return 0 elements because allTextContents() OR all() does not wait for elements to match the locator,
    // and instead immediately returns whatever is present on the page
    
    // var productTitles = await cardTitles.all();
    // console.log(productTitles);
    // productTitles.forEach(async el => {
    //     console.log(await el.textContent());
    // });
    
    // var productTitles = await cardTitles.allTextContents();
    // console.log(productTitles); 
    
    // This works because textContent() waits for elements to match the locator
    console.log(await page.locator('.card-body .card-title').nth(0).textContent());
    console.log(await page.locator('.card-body .card-title').nth(1).textContent());
});

test.only('Handling static Select Dropdowns', async ({ page }) => {
    await page.goto("https://rahulshettyacademy.com/loginpagePractise/");

    const dropdown = page.locator("select.form-control");
    await dropdown.selectOption("consult");

    //Test User Radio
    const userRadio = page.locator("[type='radio']").last();
    await userRadio.click();
    await page.locator('#okayBtn').click();
    await expect(userRadio).toBeChecked();

    //Test Terms Radio
    const terms = page.locator('#terms');
    await terms.click();
    await expect(terms).toBeChecked();

    await terms.uncheck();
    expect(await terms.isChecked()).toBeFalsy();

    await page.pause();
});