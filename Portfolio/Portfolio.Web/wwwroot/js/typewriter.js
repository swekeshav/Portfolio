document.addEventListener("DOMContentLoaded", function (event) {

    //Source: https://css-tricks.com/snippets/css/typewriter-effect/ <= Use in Readme

    function setupTypewriter(t) {
        var HTML = t.innerHTML;

        t.innerHTML = "";

        var cursorPosition = 0,
            tag = "",
            writingTag = false,
            tagOpen = false,
            typeSpeed = 50,
            tempTypeSpeed = 0;

        var type = function () {

            if (writingTag === true) {
                tag += HTML[cursorPosition];
            }

            if (HTML[cursorPosition] === "<") {
                tempTypeSpeed = 0;
                if (tagOpen) {
                    tagOpen = false;
                    writingTag = true;
                } else {
                    tag = "";
                    tagOpen = true;
                    writingTag = true;
                    tag += HTML[cursorPosition];
                }
            }
            if (!writingTag && tagOpen) {
                tag.innerHTML += HTML[cursorPosition];
            }
            if (!writingTag && !tagOpen) {
                if (HTML[cursorPosition] === " ") {
                    tempTypeSpeed = 0;
                }
                else {
                    tempTypeSpeed = (Math.random() * typeSpeed) + 50;
                }
                t.innerHTML += HTML[cursorPosition];
            }
            if (writingTag === true && HTML[cursorPosition] === ">") {
                tempTypeSpeed = (Math.random() * typeSpeed) + 50;
                writingTag = false;
                if (tagOpen) {
                    var newSpan = document.createElement("span");
                    t.appendChild(newSpan);
                    newSpan.innerHTML = tag;
                    tag = newSpan.firstChild;
                }
            }

            cursorPosition += 1;
            if (cursorPosition < HTML.length - 1) {
                setTimeout(type, tempTypeSpeed);
            } else {
                setTimeout(showPortfolioItems, 500);
            }
        };

        return {
            type: type
        };
    }

    let isFirstLoad = localStorage.getItem('isFirstLoad');
    localStorage.setItem('isFirstLoad', true);

    if (isFirstLoad == null) {
        typewriter = setupTypewriter(typewriter);
        typewriter.type();
    } else {
        let typer = document.getElementById('typewriter');
        typer.classList.add('jsLoaded');
        showPortfolioItems();
    }

    function showPortfolioItems() {
        var items = document.getElementById('portfolio-items');
        items.classList.remove('d-none');
    }
});