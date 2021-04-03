// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

async function  postAsync(url, data)
{
    let response = await fetch(
        url,
        {
            method: 'post',
            body: new URLSearchParams(data)
        });

    if (!response.ok)
    {
        let error = response.statusText || await response.text();
        return {ok:false, statusText: response.statusText, status:response.status, error:error};
    }
    let result = await response.json();
    return {ok:true, statusText: response.statusText, status:response.status, result: result};

}

//export  {postAsync}