const uri = '/Task';
let tasks = [];

const getToken=()=>{return sessionStorage.getItem('token').replace(/"/g, '');}

const getItems=()=>{
    const token= getToken();
    fetch(uri,{
        headers:{'Authorization': 'Bearer '+ token}
    })
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

const addItem=()=> {
    const addNameTextbox = document.getElementById('add-name');
    const item = {
        doneOrNot: false,
        name: addNameTextbox.value.trim()
    };
    const token= getToken();;
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

const deleteItem=(id)=> {
    const ok=confirm("Are you sure to remove this user?")
    if(ok){
    const token= getToken();
    fetch(`${uri}/${id}`, {
            method: 'DELETE',
            headers:{'Authorization': 'Bearer '+ token}
        })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}}

const displayEditForm=(id)=> {
    const item = tasks.find(item => item.id === id);

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-doneOrNot').checked = item.doneOrNot;
    document.getElementById('editForm').style.display = 'block';
}

const updateItem=()=> {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        doneOrNot: document.getElementById('edit-doneOrNot').checked,
        name: document.getElementById('edit-name').value.trim()
    };
    const token= getToken();;
    fetch(`${uri}/${itemId}`, {
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
    const name = (itemCount === 1) ? 'task' : 'task kinds';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

const _displayItems=(data)=> {
    const tBody = document.getElementById('tasks');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let doneOrNotCheckbox = document.createElement('input');
        doneOrNotCheckbox.type = 'checkbox';
        doneOrNotCheckbox.disabled = true;
        doneOrNotCheckbox.checked = item.doneOrNot;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(doneOrNotCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    tasks = data;
}