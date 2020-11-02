pipeline {
  agent any
  stages {
    stage('Verify Branch') {
      steps {
        echo "$GIT_BRANCH"
      }
    }
    // stage('Run Unit Tests') {
    //   steps {
    //     sh(script: """
    //       cd Server
    //       dotnet test
    //       cd ..
    //     """)
    //   }
    // }
    stage('Docker Build') {
      steps {
        sh(script: 'docker-compose build')
        sh(script: 'docker images -a')
      }
    }
    stage('Run Test Application') {
      steps {
        sh(script: 'docker-compose up -d')
      }
    }
    stage('Run Integration Tests') {
      steps {
        sh "bash ./Tests/ContainerTests.sh"
      }
    }
    stage('Stop Test Application') {
      steps {
        sh(script: 'docker-compose down')
        // sh(script: 'docker volumes prune -f')
      }
      post {
        success {
          echo "Build successfull! You should deploy! :)"
        }
        failure {
          echo "Build failed! You should receive an e-mail! :("
        }
       }
     }
    // stage('Push Images') {
    //   when { branch 'main' }
    //   steps {
    //     script {
    //       docker.withRegistry('https://index.docker.io/v1/', 'DockerHub') {
    //         def image = docker.image("ivaylokenov/carrentalsystem-identity-service")
    //         image.push("1.0.${env.BUILD_ID}")
    //         image.push('latest')
    //       }
    //     }
    //   }
    // }
  }
}