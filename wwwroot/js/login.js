const uri='/User';
let data;


const login = () => {
    const uName = document.getElementById('userName').value;
    const pwd = document.getElementById('password').value;
    const date = new Date()
    const newUser = {
        UserName: uName,
        Password: pwd
    }
    if (pwd === "S" + date.getFullYear() + "#" + date.getDate() + "!") {
        newUser.Admin = true;

        fetch(uri+'/Login', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newUser)
        })
        .then(response => response.json())
        .then((d) => {
            data=d;
            console.log("headers",response.getHeaders() );
            uName.value = '';
            pwd.value='';
        })
        .catch(error => console.error('Unable to login user.', error));
    }
    else { newUser.Admin = false; }

    


}


