const uri = '/User';
let data;
const container = document.getElementById('container');


const login =() => {
    const uName = document.getElementById('userName').value;
    const pwd = document.getElementById('password').value;
    const user = {
        UserName: uName,
        Password: pwd,
        Admin: false
    }
   fetch(uri + '/Login', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    }).then(response =>response.json())
        .then((data) => {
            sessionStorage.setItem("token",JSON.stringify(data.token) );
            if(data.admin===true){
                location.href="../data/usersManagement.html";
            }
            else{
                location.href="../data/tasksManagment.html";
            }
           
        })
        .catch(error => console.error('Unable to login user.', error));

}


