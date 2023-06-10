const uri = '/User';
let users = [];

const getToken=()=>{return sessionStorage.getItem('token').replace(/"/g, '');}

const getItems=()=> {
    const token= getToken();
    fetch(uri,{
        headers:{'Authorization': 'Bearer '+ token}

    })
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

const addItem=()=> {
    const addNameTextbox = document.getElementById('add-userName').value;
    const addPasswordTextbox = document.getElementById('add-userPassword').value;
console.log(addNameTextbox,addPasswordTextbox);
    const item = {
        admin: false,
        userName: addNameTextbox,
        password: addPasswordTextbox
    };
    const token= getToken();
    fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer '+ token
            },
            body: JSON.stringify(item)
        })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

const deleteItem=(password)=> {
    const ok=confirm("Are you sure to remove this user?")
    if(ok){
    const token= getToken();
    fetch(`${uri}/${password}`, {
            method: 'DELETE',
            headers:{'Authorization': 'Bearer '+ token}
        })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}}

const displayEditForm=(password)=> {
    const item = users.filter(item => item.password == password)[0];
    document.getElementById('edit-userName').value = item.userName;
   // document.getElementById('edit-password').type='text';
    document.getElementById('edit-password').value = item.password;
    document.getElementById('edit-admin').checked = item.admin;
    document.getElementById('editForm').style.display = 'block';
}

const updateItem=()=> {
    const itemPassword = document.getElementById('edit-password').value;
    const item = {
        userName: document.getElementById('edit-userName').value.trim(),
        password: itemPassword,
        admin: document.getElementById('edit-admin').checked,
    };
    const token= getToken();
    fetch(`${uri}/${itemPassword}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer '+ token
            },
            body: JSON.stringify(item)
        })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

const closeInput=()=> {
    document.getElementById('editForm').style.display = 'none';
}

const _displayCount=(itemCount)=> {
    const name = (itemCount === 1) ? 'user' : 'users';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

const _displayItems=(data)=> {
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let adminCheckbox = document.createElement('input');
        adminCheckbox.type = 'checkbox';
        adminCheckbox.disabled = true;
        adminCheckbox.checked = item.admin;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.password})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.password})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(adminCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.userName);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    users = data;
}