const liElements = document.querySelectorAll(".list_type_doc li");
liElements.forEach(li => {
    li.addEventListener("click", event => {
        const loaiTaiLieu = li.getAttribute("data-type");
        liElements.forEach(i => i.classList.remove("item_selected"));
        event.target.classList.add("item_selected");
        showNumberPages(loaiTaiLieu, 1);
    });
});
const addEffectClickPage = () => {
    const lstItemPage = document.querySelectorAll(".pagination-homepage li");
    lstItemPage.forEach(item => {
        item.addEventListener("click", event => {
            if (item.textContent == '«')
                showPreviousPage()
            else if (item.textContent == '»')
                showNextPage()
            else {
                if (item.textContent != currentPage) {
                    currentPage = item.textContent
                    lstItemPage.forEach(i => i.classList.remove("active"));
                    event.target.classList.add("active");
                    changeNumberPage(currentPage, numberItemPerPage)
                }

            }
        })
    });
}
const changeNumberPage = (currentPage, pageSize) => {
    const start = (currentPage - 1) * pageSize
    const end = start + pageSize
    const listDocumentToDisplay = allDocuments.slice(start, end)

    const danhSachHTML = listDocumentToDisplay.map(item => `
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
const showNextPage = () => {
    if (currentPage == lastPage)
        increasePageList()
    else {
        currentPage++;
        const lstItemPage = document.querySelectorAll(".pagination-homepage li");
        lstItemPage.forEach(item => {
            if (item.textContent == currentPage)
                item.classList.add('active')
            else
                item.classList.remove('active')
        });
    }
    changeNumberPage(currentPage, numberItemPerPage)
}
const showPreviousPage = () => {
    if (currentPage == firstPage)
        decreasePageList()
    else {
        currentPage--;
        const lstItemPage = document.querySelectorAll(".pagination-homepage li");
        lstItemPage.forEach(item => {
            if (item.textContent == currentPage)
                item.classList.add('active')
            else
                item.classList.remove('active')
        });
    }
    changeNumberPage(currentPage, numberItemPerPage)
}
const decreasePageList = () => {
    if (firstPage > 1) {
        let listElementPage = document.querySelectorAll('.pagination-homepage li')
        listElementPage.forEach(item => {
            if (item.textContent != '«' && item.textContent != '»') {
                item.textContent = Number(item.textContent) - 1
            }

        })
        currentPage--;
        firstPage--;
        lastPage--;
    }
}
const increasePageList = () => {
    if (lastPage < totalPages) {
        let listElementPage = document.querySelectorAll('.pagination-homepage li')
        listElementPage.forEach(item => {
            if (item.textContent != '«' && item.textContent != '»') {
                item.textContent = Number(item.textContent) + 1
            }

        })
        currentPage++;
        lastPage++;
        firstPage++;
    }
}
const showNumberPages = async (loaiTaiLieu, currentPage) => {
    const url = `/TaiLieu/GetDocumentByCategory?type=${loaiTaiLieu}`;
    const response = await fetch(url, { method: 'GET' });
    allDocuments = await response.json()

    changeNumberPage(currentPage, numberItemPerPage)

    totalPages = Math.ceil(allDocuments.length / numberItemPerPage)
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
    addEffectClickPage()
}
const truncate = (str, maxLines) => {
    const words = str.split(' ');
    if (words.length > maxLines) {
        return words.slice(0, maxLines).join(' ') + '...';
    }
    return str;
}
const viewDetail = (id) => {
    const url = `/TaiLieu/Detail?id=${id}`;
    window.location.href = url;
}

let lastPage = 0, firstPage = 0, currentPage = 1, totalPages = 0;
const numberItemPerPage = 12, limitedDisplayPage = 2;
let allDocuments = []
showNumberPages("book", 1);



