window.addEventListener('load', () =>
{
    // Find all the elements on screenw ith the specified CSS selector. In our
    // case, we are looking for all the items with the add-item-button class.
    let addButtons = document.querySelectorAll(".add-item-button");
    // Loop through each of the found elements.
    addButtons.forEach((item) =>
    {
    // Get the product ID from the value attribute of the button.
    let productId = parseInt(item.getAttribute("Value"));
    // Attach a click event listener to the button.
    item.addEventListener('click', () => addItemToCart(productId));
});

});

// Function to add an item to the shopping cart.
async function addItemToCart(productId)
{
    // Send a POST request to the server to add the item to the cart.
    let result = await fetch("/ShoppingCart/AddToCart?productId=" + productId,
        {
            method: "POST"
        }   
    );
    // Check the result of the request.
    if (result.status == 401)
    {
        // If the user is not authenticated, redirect to the login page.
        location.href = "/Authentication/Login";
    }
        // If the request was not successful, show an error message.
    else if (result.status != 200)
    {
        alert("Something went wrong!");
    }
    // Refresh the shopping cart display.
    showShoppingCart();
}