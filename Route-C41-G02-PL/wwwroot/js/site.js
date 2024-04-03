// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var searchInp = document.getElementById("searchInp");

searchInp.addEventListener("keyup", function{
    let xml = new XMLHttpRequest();
    let url = `https://localhost:44366/Employee/Index?searchInp=${searchInp.value}`;

    xml.onreadystatechange = function{
        if (this.readyState == 4 && this.status == 200) {
            console.log(this.responseText);
        }
    }
    xml.send();
})
