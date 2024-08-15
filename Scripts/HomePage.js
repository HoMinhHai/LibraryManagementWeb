
let lastPage = 0, firstPage = 0, currentPage = 0;

fetchData("book");
showNumberPages();


function addClickPage() {
    const lstItemPage = document.querySelectorAll(".pagination-homepage li");
    lstItemPage.forEach(item => {
        item.addEventListener("click", event => {
            if (item.innerHTML == '«' || item.innerHTML == '»')
                return;
            currentPage = item.textContent
            lstItemPage.forEach(i => i.classList.remove("active"));
            event.target.classList.add("active");
            changeNumberPage()
        })
        
    });
}
const changeNumberPage = () => {
    alert("press the button ")
}
function scrollListPages(symbol) {
    if (symbol == '»')
        alert(lastPage)
    else
        alert(firstPage)
}

const liElements = document.querySelectorAll(".list_type_doc li");
liElements.forEach(li => {
    li.addEventListener("click", event => {
        const loaiTaiLieu = li.getAttribute("data-type");
        liElements.forEach(i => i.classList.remove("item_selected"));
        event.target.classList.add("item_selected");
        fetchData(loaiTaiLieu);
    });
});

   

async function showNumberPages() {
    const url = `/TaiLieu/GetDocumentByCategory?type=book`;
    const response = await fetch(url, { method: 'GET' });
    const data = await response.json()

    const numberItemPerPage = 12, limitedDisplayPage = 2;
    let totalPages = Math.ceil(data.length / numberItemPerPage)

    const paginationElement = document.querySelector('.pagination-homepage')
    paginationElement.innerHTML = `<li>&laquo;</li>`
    paginationElement.innerHTML += `<li class = "active">1</li>`
    if (totalPages > limitedDisplayPage) {
        for (let i = 2; i <= limitedDisplayPage; i++) {
            paginationElement.innerHTML += `<li>${i}</li>`
        }  
        lastPage = limitedDisplayPage;
    }
    else {
        for (let i = 2; i <= totalPages; i++) {
            paginationElement.innerHTML += `<li>${i}</li>`
        }
        lastPage = totalPages;
    }
    firstPage = 1
    paginationElement.innerHTML += `<li>&raquo;</li>`
    addClickPage()
}
async function fetchData(loaiTaiLieu) {
    const params = {
        method: 'GET',
    };
    const url = `/TaiLieu/GetDocumentByCategory?type=${loaiTaiLieu}`;
    const response = await fetch(url, params);
    const data = await response.json();
    const danhSachHTML = data.map(item => `
                <div onClick ="viewDetail(${item.ID_TL})" class = "col-3 recommend_books">
                    <div style ="background-color: white;cursor: pointer; margin-right: 5px">
                        <img src="/Content/images/books/${item.AnhBia}" style="width:164px; height:235px; display:block;margin: auto" />
                        <b title="${item.TenTaiLieu}">${truncate(item.TenTaiLieu, 10)}</b>
                        <div class = "d-flex justify-content-between">
                        <p>${item.tg}</p>
                        <button id="btn-detail" class= "btn btn-primary">Chi tiết</button>
                        </div>

                    </div>
                </div>
            `).join("");
    document.getElementById("matching_books").innerHTML = danhSachHTML;
}
function truncate(str, maxLines) {
    const words = str.split(' ');
    if (words.length > maxLines) {
        return words.slice(0, maxLines).join(' ') + '...';
    }
    return str;
}
function viewDetail(id) {
    const url = `/TaiLieu/Detail?id=${id}`;
    window.location.href = url;
}

