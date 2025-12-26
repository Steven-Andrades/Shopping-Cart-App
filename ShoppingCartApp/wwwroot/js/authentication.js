// Add a listener to the form to make the page run the code below once it finishes loading.
window.addEventListener('load', () =>
{
    // Find the create user form and add a listener for when it is submitting.
    document.getElementById('createUserForm').addEventListener('submit', (e) =>
    {
        validatePassword(e)
    })
});
// Checks the password and confirmation fields to make sure they match.
async function validatePassword(e)
{
    // Get the contents of the password and confirmation fields.
    var password = document.getElementById('createUserPassword').value;
    var passwordConfirmation = document.getElementById('createUserConfirmation').value;

    // Check if the password confirmation match, if not run this.
    if (password !== passwordConfirmation)
    {
        // Prevent the event's default action, which will sstop the form from submitting.
        e.preventDefault();

        var message = document.getElementById("userFormMessage");
        message.innerText = "Password and Confirmation does not match!";

        return;
        
    }
}
