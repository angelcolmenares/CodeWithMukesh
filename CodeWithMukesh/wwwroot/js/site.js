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

async function  getAsync({ origin, pathname, data={}, targetElement }={})
{    
    let response = await fetch(
        buildUrl(origin, pathname, data),
        {
            method: 'get',
        });

    if (!response.ok)
    {
        let error = response.statusText || await response.text();
        return {ok:false, statusText: response.statusText, status:response.status, error:error};
    }
    let result = await response.json();
    return {ok:true, statusText: response.statusText, status:response.status, result: result};

}

async function loadAsync({ origin, pathname, data={}, targetElement }={})
{
    let response = await fetch(
        buildUrl(origin, pathname, data),
        {
            method: 'get',
        });

    if (!response.ok)
    {
        let error = response.statusText || await response.text();
        targetElement.innerHTML= `status: ${response.status}  statusText: ${error}`
        return;    
    }
    let result = await response.text();
    targetElement.innerHTML =  result;
}


function buildUrl( origin, pathname, data)
{
    origin= origin|| location.origin;
    const response = new URL( `${origin}${pathname}`);
    response.search = new URLSearchParams(data).toString();
    console.log(response);
    return response;
}
/*
var url = new URL('https://sl.se')

var params = {lat:35.696233, long:139.570431} // or:
var params = [['lat', '35.696233'], ['long', '139.570431']]

url.search = new URLSearchParams(params).toString();

fetch(url)

fetch('https://example.com?' + new URLSearchParams({
    foo: 'value',
    bar: 2,
}))

*/
//export  {postAsync}