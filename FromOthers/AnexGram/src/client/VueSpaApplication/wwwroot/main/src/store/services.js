import Axios from 'axios'
import UserService from '../services/UserService'
import PhotoService from '../services/PhotoService'
import LikeService from '../services/LikeService'
import CommentService from '../services/CommentService'
import ReportService from '../services/ReportService'
import FileService from '../services/FileService'

// Axios Configuration
Axios.defaults.headers.common.Accept = 'application/json'
Axios.defaults.headers.common.Authorization = `bearer ${User.Token}`

export default {
    userService: new UserService(Axios, window.Api.url),
    photoService: new PhotoService(Axios, window.Api.url),
    likeService: new LikeService(Axios, window.Api.url),
    commentService: new CommentService(Axios, window.Api.url),
    reportService: new ReportService(Axios, window.Api.url),
    fileService: new FileService(),
}