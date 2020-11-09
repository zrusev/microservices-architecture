pipeline {
  agent any
  stages {
    stage('Verify Branch') {
      steps {
        echo "$GIT_BRANCH"
      }
    }
    stage('Run Unit Tests') {
      steps {
        sh(script: """
          cd Server
          dotnet test
          cd ..
        """)
      }
    }
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
      parallel(
        stage("stage Startup") {
          steps {
            try {
				      sh "bash ./Tests/Startup.sh"
            } catch (Exception e) {
              currentBuild.result = 'UNSTABLE'
            }
          }
        },
        stage("stage Register") {
          steps {
            try {
				      sh "bash ./Tests/Register.sh"
            } catch (Exception e) {
              currentBuild.result = 'UNSTABLE'
            }
          }
        },
        stage("stage Login") {
          steps {
            try {
				      sh "bash ./Tests/Login.sh"
            } catch (Exception e) {
              currentBuild.result = 'UNSTABLE'
            }
          }
        },
        stage("stage TopProducts") {
          steps {
            try {
				      sh "bash ./Tests/TopProducts.sh"
            } catch (Exception e) {
              currentBuild.result = 'UNSTABLE'
            }
          }
        },
        stage("stage Statistics") {
          steps {
            try {
              sh "bash ./Tests/Statistics.sh"
            } catch (Exception e) {
              currentBuild.result = 'UNSTABLE'
            }
          }
        }
      )
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
    stage('Push Images') {
      when { branch 'master' }
      steps {
        script {
          def images = [
            'zlatkorusev/microservices-architecture-identity-service',
            'zlatkorusev/microservices-architecture-customers-service',
            'zlatkorusev/microservices-architecture-statistics-service',
            'zlatkorusev/microservices-architecture-orders-service',
            'zlatkorusev/microservices-architecture-admin-service',
            'zlatkorusev/microservices-architecture-customers-gateway-service',
            'zlatkorusev/microservices-architecture-notifications-service',
            'zlatkorusev/microservices-architecture-monitoring-service',
            'zlatkorusev/microservices-architecture-client']

          for (int i = 0; i < images.size(); ++i) {
            docker.withRegistry('https://index.docker.io/v1/', 'DockerHub') {
              def image = docker.image(images[i])
              image.push("1.0.${env.BUILD_ID}")
              image.push('latest')
            }
          }
        }
      }
    }
    stage('Send Success Email') {
      steps {
        script {
            emailext (
              subject: "SUCCESSFUL: Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]'",
              body: """<p>SUCCESSFUL: Job '${env.JOB_NAME} [${env.BUILD_NUMBER}]':</p>
                <p>Check console output at <a href='${env.BUILD_URL}'>${env.JOB_NAME} [${env.BUILD_NUMBER}]</a></p>""",
              recipientProviders: [
                [$class: 'DevelopersRecipientProvider']
              ],
              replyTo: '$DEFAULT_REPLYTO',
              to: '$DEFAULT_RECIPIENTS'
            )
        }
      }
    }
  }
}