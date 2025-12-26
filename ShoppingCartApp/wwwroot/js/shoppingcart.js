// Add a listener to the window which will run the contained code once it finishes loading.
window.addEventListener('load', () =>
{
    // Find the component named btnCart and add a listener to it that will run the attached method when pressed.
    document.getElementById('btnCart').addEventListener('click', () => showShoppingCart());
})
// Function to show the shopping cart offcanvas component.
async function showShoppingCart()
{
    // Send a fetch request to our controller to run the index method and retreve our partial view.
    var result = await fetch("/ShoppingCart/Index");
    // Check the status code of the response and redirect the user to the login page if the
    // unauthorised response occured, an alert on any other code that is not a 200/OK.
    if (result.status == 401)
    {
        location.href = "/Authentication/Login";
    }
    else if (result.status != 200)
    {
        alert("Something went wrong!");
        return;
    }
    // Get the body content out of the response as text. This will be our partial view for the cart.
    var htmlResult = await result.text();
    // Find the shopping cart body by using its ID then put the partial view text between the ages of the
    // elements.
    document.getElementById('shoppingCartBody').innerHTML = htmlResult;
    // Disable the line item quantity forms to prevent multiple submissions.
    disableLineItemForms();
    // Setup the quantity buttons to allow changing of line item quantities.
    setupQuantityButtons();
    // Setup the remove buttons to allow removing of line items.
    setupRemoveButtons();
    // Setup the checkout and cancel buttons.
    setupCheckoutAndCancelButtons();
    // Recalculate the cart total to ensure it is up to date.
    recalculateCartTotal();
    // Use JQuery to find the shopping cart using its ID then run the show operation from the
    // offcanvas interface.
    $('#shoppingCart').offcanvas('show');
}

// Function to disable the line item quantity forms to prevent multiple submissions.
async function disableLineItemForms()
{
    let itemQtyForms = document.querySelectorAll(".cart-qty-toggle");
    itemQtyForms.forEach((form) =>
    {
        form.addEventListener('submit', (e) =>
        {
            e.preventDefault();
        })
    })
}
// Function to change the quantity of a line item in the shopping cart.
async function setupQuantityButtons()
{
    // Find all the minus buttons and attach event listeners to them.
    let minusButtons = document.querySelectorAll(".btn-minus");
    minusButtons.forEach((button) =>
    {
        button.addEventListener('click', (e) => changeQuantity(e, -1))
    });
    // Find all the plus buttons and attach event listeners to them.
    let plusButtons = document.querySelectorAll(".btn-plus");
    plusButtons.forEach((button) =>
    {
        button.addEventListener('click', (e) => changeQuantity(e, 1))
    });
}
// Function to update the quantity of a line item in the database.
async function changeQuantity(e, amount)
{
    // Get the cart item ID from the hidden input field in the form.
    let cartItemId = parseInt(e.target.form.querySelector("input").value);
    // Get the current quantity from the quantity text element. Grab text 
    // between the tags.
    let qty = parseInt(e.target.form.querySelector(".qty-text").innerText);
    // Update the quantity in the database.
    qty += amount;
    // If the quantity is less than 1, remove the item from the cart.
    if (qty < 1)
    {
        removeItem(e, cartItemId);
        return;
    }
    // Ensure the quantity does not go below 1.
    e.target.form.querySelector(".qty-text").innerText = qty;
    // Recalculate the line total for the item.
    recalculateLineTotal(e, qty);
    // Recalculate the cart total.
    recalculateCartTotal();
    //  Call the function to update the quantity in the database.
    updateQuantityInDatabase(qty, cartItemId);
}
// Function to update the quantity of a line item in the database.
async function updateQuantityInDatabase(qty, cartItemId)
{
    // Create the updated item object.
    let updatedItem =
    {
        Id: cartItemId,
        Quantity: qty
    }
    // Send the PUT request to the server to update the item quantity.
    let response = await fetch("/ShoppingCart/UpdateItemQuantity",
        {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(updatedItem)
        })
    // Check the response status code for errors.
    if (response.status != 200)
    {
        alert("Something went wrong!");
    }
}
// Function to remove an item from the shopping cart.
async function setupRemoveButtons()
{
    let removeButtons = document.querySelectorAll(".remove-button");
    removeButtons.forEach((button) => {

        let itemId = parseInt(button.getAttribute("value"));
        button.addEventListener('click', (e) => removeItem(e, itemId));
        // Recalculate the cart total.
        recalculateCartTotal();
    });
}
// Function to remove an item from the shopping cart.
async function removeItem(e, itemId)
{
    // Send the DELETE request to the server to remove the item.
    let response = await fetch("/ShoppingCart/RemoveFromItem?Id=" + itemId, {
            method: "DELETE"
    });
    //  Check the response status code for errors.
    if (response.status != 200) {
        alert("Something went wrong!");
        return;
    }
    // Remove the item from the shopping cart display.
    let parent = e.target.closest(".card");
    parent.remove();   
    recalculateCartTotal();
}
// Recalculate the shopping cart total.
async function recalculateLineTotal(e, qty) {
    let lineItem = e.target.form.querySelector(".line-total");
    let unitPrice = parseFloat(lineItem.getAttribute("value"));

    let totalPrice = Number(unitPrice * qty);
    lineItem.innerText = "$" + totalPrice.toFixed(2);
}
// Recalculate the shopping cart total.
async function recalculateCartTotal()
{
    let lineItems = document.querySelectorAll(".line-total");
    let total = 0.00;

    lineItems.forEach((item) =>
    {
        let linePrice = parseFloat(item.innerHTML.replace("$", ""))
        total += linePrice;
    });

    if (lineItems.length == 0) {
        document.querySelector("#btnCheckout").remove();
        document.querySelector("#btnCancel").remove();
    }

    document.getElementById("cartTotal").innerHTML = "$" + total.toFixed(2) +
                            " <strong>.inc GST</strong>"
}

// Function to setup the checkout and cancel buttons.
async function setupCheckoutAndCancelButtons() {
    let checkoutButton = document.querySelector("#btnCheckout");
    // Add event listener to checkout button.
    if (checkoutButton != null) {
        checkoutButton.addEventListener('click', (e) => finaliseCart(e));
    }

    let cancelButton = document.querySelector("#btnCancel");
    // Add event listener to cancel button.
    if (cancelButton != null) {
        cancelButton.addEventListener('click', (e) => cancelCart(e));
    }
}

// Function to cancel the cart and close the offcanvas.
async function finaliseCart(e) {
    if (confirm("Proceed with Payment?") == true) {
        //  Get the cart ID from the button value attribute.
        let cartId = parseInt(e.target.getAttribute("value"));
        // Send the PUT request to finalise the cart.
        let result = await fetch("/ShoppingCart/FinaliseCart?id=" + cartId,
            {
                method: "PUT"
            });
        // Check the response status code for errors.
        if (result.status != 200) {
            alert("Something went wrong!");
        }
        // If successful, close the cart and alert the user.
        else {
            $("#shoppingCart").offcanvas('hide');
            alert("Cart Finalised Successfully! Thank you for shopping with us.");
        }
    }
}

async function cancelCart(e) {
    if (confirm("Cancel Cart?") == true) {
        let cartId = parseInt(e.target.getAttribute("value"));

        let result = await fetch("/ShoppingCart/CancelCart?id=" + cartId,
            {
                method: "DELETE"
            });
        if (result.status != 200) {
            alert("Something went wrong!");
        }
        else {
            $("#shoppingCart").offcanvas('hide');
            alert("Cart Cancelled.");
        }
    }
}