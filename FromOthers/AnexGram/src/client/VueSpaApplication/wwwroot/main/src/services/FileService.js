class FileService {

  constructor() {

  }

  get(file) {
    let reader = new FileReader();
    reader.readAsDataURL(file);

    return new Promise((resolve, reject) => {
      try {
        reader.onload = () => {
          resolve({
            content: reader.result.split(',')[1],
            name: file.name,
            length: file.size,
            contentType: file.type
          })
        }
      } catch (error) {
        reject(error)
      }
    })
  }
}

export default FileService
